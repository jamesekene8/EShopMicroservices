﻿

namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required"); ;
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }
    
        
    
    internal class CreateProductCommandHandler(IDocumentSession session) 
		: ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);


			//return CreateProductResult with Id
			return new CreateProductResult(product.Id);

			
		}
	}
}
