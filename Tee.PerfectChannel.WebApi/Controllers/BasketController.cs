using System.Web.Http;
using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    public class BasketController : ApiController
    {
        public IHttpActionResult GetBasket()
        {
            return Ok(new Basket());
        }
    }
}