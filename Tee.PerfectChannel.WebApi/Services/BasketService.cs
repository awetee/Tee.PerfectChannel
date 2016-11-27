using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketDataService;

        public BasketService(IRepository<Basket> basketDataService)
        {
            _basketDataService = basketDataService;
        }

        public Basket Get(int id)
        {
            return this._basketDataService.Get(id);
        }

        public void Update(Basket basket)
        {
            this._basketDataService.Update(basket);
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