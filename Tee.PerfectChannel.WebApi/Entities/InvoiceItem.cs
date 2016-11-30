namespace Tee.PerfectChannel.WebApi.Entities
{
    public class InvoiceItem : IBaseEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double PricePerItem { get; set; }
        public double Price => Quantity * PricePerItem;
        public int InvoiceId { get; set; }
    }
}