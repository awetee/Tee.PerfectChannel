using System.Linq;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Extensions;
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
            Guard.AgainstNull(basket, "basket should not be null");

            this._basketDataService.Update(basket);
        }
    }
}