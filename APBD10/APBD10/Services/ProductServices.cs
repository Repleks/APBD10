using System.ComponentModel.DataAnnotations;
using APBD10.Context;
using APBD10.Models;
using APBD10.RequestModels;
using APBD10.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services
{
    public class ProductServices
    {
        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message)
            {
            }
        }
        private readonly ShopContext _context;
    
        public ProductServices(ShopContext context)
        {
            _context = context;
        }

        public async Task<ProductResponseModel> PostProductWithCategories(ProductRequestModel productWithCategories)
        {
            var product = new Product
            {
                Name = productWithCategories.Name,
                Weight = productWithCategories.Weight,
                Width = productWithCategories.Width,
                Height = productWithCategories.Height,
                Depth = productWithCategories.Depth
            };
            var categoriesId = productWithCategories.CategoriesId;
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
            if (existingProduct != null)
            {
                throw new ConflictException("Product with the same name already exists");
            }

            var validationContext = new ValidationContext(product);
            if (!Validator.TryValidateObject(product, validationContext, null, true))
            {
                throw new ValidationException("Product is not valid");
            }   
        
            foreach (var categoryId in categoriesId)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
                if (!categoryExists)
                {
                    throw new ConflictException($"Category with id {categoryId} does not exist");
                }
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                foreach (var categoryId in categoriesId)
                {
                    await _context.ProductsCategories.AddAsync(new ProductCategories
                    {
                        ProductId = product.ProductId,
                        CategoryId = categoryId
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return new ProductResponseModel
            {
                Name = product.Name,
                Weight = product.Weight,
                Width = product.Width,
                Height = product.Height,
                Depth = product.Depth,
                CategoriesId = categoriesId
            };
        }
    }
}
