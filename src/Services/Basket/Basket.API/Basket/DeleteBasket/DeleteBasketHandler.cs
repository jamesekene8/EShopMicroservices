namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool IsSuccess);

	public class DeleteBasketDeleteCommandValidator : AbstractValidator<DeleteBasketCommand>
	{
		public DeleteBasketDeleteCommandValidator()
		{
			RuleFor(x => x.UserName).NotNull().WithMessage("Username is required");
		}
	}	

	public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			await repository.DeleteBasket(command.UserName, cancellationToken);
			return new DeleteBasketResult(true);
		}
	}
}
