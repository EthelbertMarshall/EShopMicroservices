
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory
{ 
   // public record GetProductByCategoryRequest(string Category);

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async(string category, ISender sender) => {

                var products = await sender.Send(new GetProductByCategoryQuery(category));

                var result = products.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(result);

            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");


        }
    }
}
