using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Controllers
{
    public class ItemController : ApiController
    {
        public IHttpActionResult Get()
        {
            var result = new List<Item>
            {
                new Item(),
                new Item(),
            };

            return Ok(result.AsEnumerable());
        }
    }
}