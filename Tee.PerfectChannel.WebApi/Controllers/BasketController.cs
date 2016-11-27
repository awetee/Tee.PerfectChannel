using System.Web.Http;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IItemService _itemService;
        private readonly IMapperService _mapperService;
        private readonly IBasketService _basketService;

        public BasketController(IItemService itemService, IMapperService mapperService, IBasketService basketService)
        {
            _itemService = itemService;
            _mapperService = mapperService;
            _basketService = basketService;
        }

        public IHttpActionResult GetBasket(int basketId)
        {
            return Ok(this._basketService.Get(basketId));
        }

        public IHttpActionResult AddItemToBasket(int basketId, int itemId, int quantity)
        {
            var item = this._itemService.Get(itemId);
            var mapped = this._mapperService.Map(item);
            mapped.Quantity = quantity;
            var result = this._basketService.AddToBasket(basketId, mapped);
            return Ok(result);
        }
    }
}