using System.Linq;
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

        public Basket GetByUserId(int id)
        {
            return this._basketDataService.GetAll().FirstOrDefault(b => b.UserId == id);
        }

        public void Update(Basket basket)
        {
            this._basketDataService.Update(basket);
        }

        public Basket AddToBasket(int userId, BasketItem basketItem)
        {
            var basket = this.GetByUserId(userId);
            basket.AddBacketItem(basketItem);
            return basket;
        }
    }
}