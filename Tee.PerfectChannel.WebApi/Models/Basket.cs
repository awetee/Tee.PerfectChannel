using System.Collections.Generic;
using System.Linq;

namespace Tee.PerfectChannel.WebApi.Models
{
    public class Basket
    {
        public IEnumerable<BasketItem> BasketItems { get; set; }
        public decimal Total => BasketItems.Sum(i => i.Cost);
    }
}