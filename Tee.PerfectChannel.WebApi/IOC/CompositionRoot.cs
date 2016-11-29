using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.IOC
{
    public class CompositionRoot : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var itemContext = new ItemsContext();
            var itemService = new ItemService(new Repository<Item>(itemContext));

            if (controllerType == typeof(ItemController))
            {
                return new ItemController(itemService);
            }

            if (controllerType == typeof(BasketController))
            {
                var mapperService = new MapperService();
                var basketService = new BasketService(new Repository<Basket>(itemContext));
                var userService = new UserService(new Repository<User>(itemContext));
                return new BasketController(itemService, mapperService, basketService, userService);
            }

            throw new ArgumentException("Unexpected type!", nameof(controllerType));
        }
    }
}