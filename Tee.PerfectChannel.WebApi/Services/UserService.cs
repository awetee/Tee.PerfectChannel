using System.Linq;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Extensions;
using Tee.PerfectChannel.WebApi.Repository;

namespace Tee.PerfectChannel.WebApi.Services
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public User Get(string userName)
        {
            Guard.AgainstNull(userName, "userName should not be null");
            return this._userRepository.GetAll().SingleOrDefault(u => u.Name == userName);
        }
    }
}