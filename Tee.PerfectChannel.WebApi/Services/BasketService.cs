using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDataService<Basket> _basketDataService;

        public BasketService(IDataService<Basket> basketDataService)
        {
            _basketDataService = basketDataService;
        }

        public Basket Get(int id)
        {
            return this._basketDataService.Get(id);
        }

        public void Update(Basket basket)
        {
            this._basketDataService.Update();
        }

        public Basket AddToBasket(int basketId, BasketItem basketItem)
        {
            var basket = this.Get(basketId);
            basket.AddBacketItem(basketItem);
            this.Update(basket);
            return basket;
        }
    }
}