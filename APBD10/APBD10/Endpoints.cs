using System.ComponentModel.DataAnnotations;
using APBD10.RequestModels;
using APBD10.Services;


namespace APBD10;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/accounts/{accountId:int}", async (AccountServices accountService, int accountId) =>
        {
            try
            {
                var account = await accountService.GetAccountById(accountId);
                if (account == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(account);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        });
        endpoints.MapPost("/api/products", async (ProductServices productService, ProductRequestModel productRequestModel) =>
        {
            try
            {
                var productResponse = await productService.PostProductWithCategories(productRequestModel);
                return Results.Created($"/api/products/{productResponse.Name}", productResponse);
            }
            catch (ProductServices.ConflictException e)
            {
                return Results.Conflict(e.Message);
            }
            catch (ValidationException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return Results.Problem(e.Message);
            }
        });
    }
}