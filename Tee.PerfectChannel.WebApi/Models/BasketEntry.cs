using Newtonsoft.Json;

namespace Tee.PerfectChannel.WebApi.Models
{
    [JsonObject]
    public class BasketEntry
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}