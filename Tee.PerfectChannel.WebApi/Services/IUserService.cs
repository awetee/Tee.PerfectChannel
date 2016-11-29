namespace Tee.PerfectChannel.WebApi.Services
{
    using Tee.PerfectChannel.WebApi.Entities;

    public interface IUserService
    {
        User Get(string userName);
    }
}