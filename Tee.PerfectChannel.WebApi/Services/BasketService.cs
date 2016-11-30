using System.Collections.Generic;
using System.Linq;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Extensions;
using Tee.PerfectChannel.WebApi.Repository;

namespace Tee.PerfectChannel.WebApi.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IRepository<BasketItem> _basketItemRepository;

        public BasketService(IRepository<Basket> basketRepository, IRepository<Invoice> invoiceRepository, IRepository<InvoiceItem> invoiceItemRepository, IRepository<BasketItem> basketItemRepository)
        {
            _basketRepository = basketRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _basketItemRepository = basketItemRepository;
        }

        public Basket GetByUserId(int id)
        {
            var basket = this._basketRepository.GetAll().FirstOrDefault(b => b.UserId == id);

            if (basket != null)
            {
                var basketItems = this._basketItemRepository.GetAll().Where(i => i.BasketId == basket.Id).ToList();
                foreach (var item in basketItems)
                {
                    basket.Add(item);
                }
                return basket;
            }

            var newBasket = new Basket { UserId = id };
            var newBasketId = this._basketRepository.Insert(newBasket);
            newBasket.Id = newBasketId;

            return newBasket;
        }

        public void Update(Basket basket)
        {
            Guard.AgainstNull(basket, "basket should not be null");

            foreach (var item in NewItems(basket))
            {
                this._basketItemRepository.Insert(item);
            }

            foreach (var item in ExistingItems(basket))
            {
                this._basketItemRepository.Update(item);
            }

            this._basketRepository.Update(basket);
        }

        private static IEnumerable<BasketItem> ExistingItems(Basket basket)
        {
            return basket.BasketItems.Where(i => i.Id > 0);
        }

        private static IEnumerable<BasketItem> NewItems(Basket basket)
        {
            return basket.BasketItems.Where(i => i.Id == 0);
        }

        public Invoice Checkout(Basket basket)
        {
            var invoice = new Invoice
            {
                UserId = basket.UserId
            };

            var invoiceId = this._invoiceRepository.Insert(invoice);

            foreach (var basketItem in basket.BasketItems)
            {
                this._invoiceItemRepository.Insert(new InvoiceItem
                {
                    ItemId = basketItem.ItemId,
                    PricePerItem = basketItem.Price,
                    Quantity = basketItem.Quantity,
                    InvoiceId = invoiceId
                });
            }

            this.Clear(basket);

            return this._invoiceRepository.Get(invoiceId);
        }

        private void Clear(Basket basket)
        {
            foreach (var basketItem in basket.BasketItems)
            {
                this._basketItemRepository.Delete(basketItem);
            }
        }
    }
}