namespace Tee.PerfectChannel.WebApi.Entities
{
    public class Item : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}