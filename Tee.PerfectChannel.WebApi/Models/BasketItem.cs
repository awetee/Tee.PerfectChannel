namespace Tee.PerfectChannel.WebApi.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost => Quantity * Price;
    }
}