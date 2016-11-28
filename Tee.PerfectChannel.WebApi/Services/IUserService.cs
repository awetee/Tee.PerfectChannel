namespace Tee.PerfectChannel.WebApi.Services
{
    using System.Linq;
    using Tee.PerfectChannel.WebApi.Entities;
    using Tee.PerfectChannel.WebApi.Repository;

    public interface IUserService
    {
        User Get(string userName);
    }

    internal class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public User Get(string userName)
        {
            return this._userRepository.GetAll().SingleOrDefault(u => u.Name == userName);
        }
    }
}