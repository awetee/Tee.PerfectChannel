using System.Web.Http;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Models;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [RoutePrefix("api/Basket")]
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

        [HttpGet]
        [Route("GetBasket/{userName}")]
        public IHttpActionResult GetBasket(string userName)
        {
            var user = this._userService.Get(userName);

            if (user == null)
            {
                return BadRequest("unknown user name");
            }

            var basket = this._basketService.GetByUserId(user.Id);

            return Ok(basket);
        }

        [HttpPost]
        [Route("AddBasketEntry/{userId}/")]
        public IHttpActionResult AddBasketEntry(int userId, [FromBody] BasketEntry basketEntry)
        {
            var errors = new List<string>();
            var basket = this._basketService.GetByUserId(userId);

            this.AddToBasket(basketEntry, errors, basket);

            if (errors.Any())
            {
                var builder = GetErrorMessage(errors);
                return this.BadRequest(builder.ToString());
            }

            this._basketService.Update(basket);
            return Ok(basket);
        }

        [HttpPost]
        [Route("AddBasketEntries/{userId}/")]
        public IHttpActionResult AddBasketEntries(int userId, [FromBody] ICollection<BasketEntry> basketEntries)
        {
            var errors = new List<string>();
            var basket = this._basketService.GetByUserId(userId);

            foreach (var basketEntry in basketEntries)
            {
                AddToBasket(basketEntry, errors, basket);
            }

            if (errors.Any())
            {
                var errorMessage = GetErrorMessage(errors);
                return this.BadRequest(errorMessage);
            }

            this._basketService.Update(basket);
            return Ok(basket);
        }

        private void AddToBasket(BasketEntry basketEntry, ICollection<string> errors, Basket basket)
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
                basket.Add(mapped);
            }
        }

        private static string GetErrorMessage(ICollection<string> errors)
        {
            var builder = new StringBuilder();

            builder.Append("Sorry, we've not got enough stock for ");

            foreach (var error in errors)
            {
                builder.Append(error != errors.Last() ? $"{error}, " : $"and {error}");
            }

            return builder.ToString();
        }
    }
}