using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASample.NetCore.Services.Customers.Domain
{
    public class Cart : AggregateRoot
    {
        private ISet<CartItem> _items = new HashSet<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get => _items;
            set => _items = new HashSet<CartItem>(value);
        }

        protected Cart()
        {
        }

        public Cart(Guid userId)
        {
            Id = userId;
            CreateTime = DateTime.Now;
        }

        public void Clear()
          => _items.Clear();

        public void AddProduct(Product product, int quantity)
        {
            var item = GetCartItem(product.Id);
            if (item != null)
            {
                item.IncreaseQuantity(quantity);
                return;
            }
            item = new CartItem(product, quantity);
            _items.Add(item);
        }

        public void UpdateProduct(Product product)
        {
            var item = GetCartItem(product.Id);
            if (item == null)
            {
                throw new ASampleException("product_not_found",
                    $"Product with id: '{product.Id}' was not found.");
            }
            item.Update(product);
        }

        public void DeleteProduct(Guid productId)
        {
            var item = GetCartItem(productId);
            if (item == null)
            {
                throw new ASampleException("product_not_found",
                    $"Product with id: '{productId}' was not found.");
            }
            _items.Remove(item);
        }

        private CartItem GetCartItem(Guid productId)
           => _items.SingleOrDefault(x => x.ProductId == productId);
    }
}
