using System.Web.Http;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IItemService _itemService;
        private readonly IMapperService _mapperService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController(IItemService itemService, IMapperService mapperService, IBasketService basketService, IUserService userService)
        {
            _itemService = itemService;
            _mapperService = mapperService;
            _basketService = basketService;
            _userService = userService;
        }

        public IHttpActionResult GetBasket(string userName)
        {
            var user = this._userService.Get(userName);
            return Ok(this._basketService.GetByUserId(user.Id));
        }

        public IHttpActionResult AddItemToBasket(int userId, int itemId, int quantity)
        {
            var item = this._itemService.Get(itemId);

            if (!ItemIsInStock(quantity, item))
            {
                return BadRequest($"Sorry, we've not got enough {item.Name} in stock");
            }

            var mapped = this._mapperService.Map(item);
            mapped.Quantity = quantity;
            var result = this._basketService.AddToBasket(userId, mapped);
            return Ok(result);
        }

        private static bool ItemIsInStock(int quantity, Item item)
        {
            return item.Stock > quantity;
        }
    }

    public interface IUserService
    {
        User Get(string userName);
    }
}