using System.Collections.Generic;
using System.Linq;

namespace Tee.PerfectChannel.WebApi.Models
{
    public class Basket
    {
        public Basket()
        {
            this.Items = new List<BasketItem>();
        }

        public int Id { get; set; }

        private List<BasketItem> Items { get; }

        public decimal Total => Items.Sum(i => i.Cost);

        public void AddBacketItem(BasketItem item)
        {
            if (this.Items.Any(i => i.ItemId == item.ItemId))
            {
                this.Items
                    .First(i => i.ItemId == item.ItemId)
                    .Quantity += item.Quantity;
            }
            else
            {
                this.Items.Add(item);
            }
        }

        public IEnumerable<BasketItem> BasketItems => this.Items.AsEnumerable();
    }
}