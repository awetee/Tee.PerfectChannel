using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi
{
    using System.Data.Entity;

    public class ItemsContext : DbContext
    {
        public ItemsContext() : base("ItemsContext")
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}