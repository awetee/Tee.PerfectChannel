using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class MapperService : IMapperService
    {
        public BasketItem Map(Item item)
        {
            return new BasketItem
            {
                ItemId = item.Id,
                ItemName = item.Name,
                Price = item.Price
            };
        }
    }
}