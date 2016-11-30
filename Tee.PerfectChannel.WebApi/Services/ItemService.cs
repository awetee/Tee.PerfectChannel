using System.Collections.Generic;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IEnumerable<Item> GetAll()
        {
            return this._itemRepository.GetAll();
        }

        public Item Get(int itemId)
        {
            return this._itemRepository.Get(itemId);
        }
    }
}