using System.Web.Http;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

            if (!item.HasEnoughInStock(quantity))
            {
                return BadRequest($"Sorry, we've not got enough {item.Name} in stock");
            }

            var mapped = this._mapperService.Map(item);
            mapped.Quantity = quantity;
            var basket = this._basketService.AddToBasket(userId, mapped);

            this._basketService.Update(basket);
            return Ok(basket);
        }

        public IHttpActionResult AddItemListToBasket(int userId, IEnumerable<BasketEntry> basketEntries)
        {
            var errors = new List<string>();
            var basket = this._basketService.GetByUserId(userId);

            foreach (var basketEntry in basketEntries)
            {
                var item = this._itemService.Get(basketEntry.ItemId);

                if (!item.HasEnoughInStock(basketEntry.Quantity))
                {
                    errors.Add(item.Name);
                }
                else
                {
                    var mapped = this._mapperService.Map(item);
                    mapped.Quantity = basketEntry.Quantity;
                    basket.AddBacketItem(mapped);
                }
            }

            if (errors.Any())
            {
                var builder = new StringBuilder();

                builder.Append("Sorry, we've not got enough of the following items in stock: ");

                foreach (var error in errors)
                {
                    builder.Append(error + ", ");
                }

                return this.BadRequest(builder.ToString());
            }

            this._basketService.Update(basket);
            return Ok(basket);
        }
    }

    public class BasketEntry
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}