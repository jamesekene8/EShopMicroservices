using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
	public record CreatProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);
	internal class CreateProductCommandHandler : ICommandHandler<CreatProductCommand, CreateProductResult>
	{
		public Task<CreateProductResult> Handle(CreatProductCommand command, CancellationToken cancellationToken)
		{
			// Business logic to create a new product
			throw new NotImplementedException();
		}
	}
}
