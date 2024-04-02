using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
	public record CreatProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);
	internal class CreateProductCommandHandler : ICommandHandler<CreatProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreatProductCommand command, CancellationToken cancellationToken)
		{
			//Create product entity from command object
			

			var product = new Product
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price
			};

			//Save to database


			//return CreateProductResult with Id
			return new CreateProductResult(Guid.NewGuid());

			
		}
	}
}
