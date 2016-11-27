namespace Tee.PerfectChannel.WebApi.Entities
{
    public class BasketItem : IBaseEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Cost => Quantity * Price;
        public int UserId { get; set; }
        public Basket Basket { get; set; }
        public Item Item { get; set; }
    }
}