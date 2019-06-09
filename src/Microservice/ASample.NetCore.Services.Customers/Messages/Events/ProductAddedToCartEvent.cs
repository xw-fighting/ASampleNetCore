using ASample.NetCore.Domain.Message;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events
{
    public class ProductAddedToCartEvent:IEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quanlity { get; set; }

        public ProductAddedToCartEvent(Guid customerId, Guid productId, int quanlity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quanlity = quanlity;
        }
    }
}
