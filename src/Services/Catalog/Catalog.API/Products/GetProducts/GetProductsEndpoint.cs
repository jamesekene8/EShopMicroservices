
namespace Catalog.API.Products.GetProducts
{
	public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
	public record GetProductByIdResponse(IEnumerable<Product> Products);


	public class GetProductsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
			{
				var query = request.Adapt<GetProductsQuery>();
				var result = await sender.Send(query);
				var response = result.Adapt<GetProductByIdResponse>();

				return Results.Ok(response);
			})
				.WithName("getProducts")
				.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get all products")
				.WithDescription("Get all products");
		}
	}
}
