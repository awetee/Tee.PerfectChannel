using System.Collections.Generic;
using Tee.PerfectChannel.WebApi.Models;

namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetAll();

        Item Get(int itemId);
    }
}