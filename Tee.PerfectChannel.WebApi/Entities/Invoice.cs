using System.Collections.Generic;
using System.Linq;

namespace Tee.PerfectChannel.WebApi.Entities
{
    public class Invoice : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public double Total => InvoiceItems.Sum(i => i.Price);
    }
}