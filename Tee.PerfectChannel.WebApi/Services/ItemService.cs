using System.Collections.Generic;
using System.Linq;
using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IDataService<Item> _dataService;

        public ItemService(IDataService<Item> dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<Item> GetAll()
        {
            return new List<Item>
            {
                new Item { Name = "Apples", Description = "Fruit", Stock = 5, Price = 2.5 },
                new Item { Name = "Bread", Description = "Loaf", Stock = 5, Price = 1.35 },
                new Item { Name = "Oranges", Description = "Fruit", Stock = 5, Price = 2.99 },
                new Item { Name = "Milk", Description = "Milk", Stock = 5, Price = 2.07 },
                new Item { Name = "Chocolate", Description = "Bars", Stock = 5, Price = 1.79 }
            };
        }

        public Item Get(int itemId)
        {
            return this.GetAll().SingleOrDefault(i => i.Id == itemId);
        }
    }
}