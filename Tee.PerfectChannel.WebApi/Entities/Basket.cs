using System.Collections.Generic;
using System.Linq;

namespace Tee.PerfectChannel.WebApi.Entities
{
    public class Basket : IBaseEntity
    {
        public Basket()
        {
            this.Items = new List<BasketItem>();
        }

        public int Id { get; set; }

        protected virtual List<BasketItem> Items { get; }

        public double Total => Items.Sum(i => i.Cost);

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