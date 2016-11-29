using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IBasketService
    {
        Basket GetByUserId(int userId);

        void Update(Basket basket);
    }
}