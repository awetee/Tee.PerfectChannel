using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IBasketService
    {
        Basket Get(int basketId);

        void Update(Basket basket);

        Basket AddToBasket(int basketId, BasketItem basketItem);
    }
}