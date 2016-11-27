using System.Web.Http;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    public class ItemController : ApiController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IHttpActionResult Get()
        {
            return Ok(this._itemService.GetAll());
        }
    }
}