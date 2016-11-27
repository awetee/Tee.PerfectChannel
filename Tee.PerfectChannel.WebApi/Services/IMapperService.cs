using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IMapperService
    {
        BasketItem Map(Item item);
    }
}