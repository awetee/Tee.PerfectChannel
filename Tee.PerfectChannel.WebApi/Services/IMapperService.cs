using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IMapperService
    {
        BasketItem Map(Item item);
    }
}