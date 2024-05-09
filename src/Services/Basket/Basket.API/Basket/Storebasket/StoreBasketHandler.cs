

namespace Basket.API.Basket.Storebasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
	public record StoreBasketResult(string UserName);

	public class StoreCommand : AbstractValidator<StoreBasketCommand>
	{
        public StoreCommand()
        {
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
			RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username is required");
        }
    }


	public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			ShoppingCart cart = command.Cart;
			await repository.StoreBasket(cart, cancellationToken);

			return new StoreBasketResult(cart.UserName);

		}
	}
}
