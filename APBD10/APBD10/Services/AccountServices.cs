using APBD10.Context;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services
{
    public class AccountServices
    {
        private readonly ShopContext _context;

        public AccountServices(ShopContext context)
        {
            _context = context;
        }

        public async Task<object?> GetAccountById(int accountId)
        {
            if(accountId <= 0)
                throw new ArgumentException("Invalid account id");
            var account = await _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.ShoppingCarts)
                .ThenInclude(sc => sc.Product)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
                return null;

            return new
            {
                account.FirstName,
                account.LastName,
                account.Email,
                account.Phone,
                Role = account.Role.Name,
                Cart = account.ShoppingCarts.Select(sc => new
                {
                    sc.ProductId,
                    ProductName = sc.Product.Name, 
                    sc.Amount
                }).ToList()
            };
        }
    }
}