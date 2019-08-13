using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.Services.Orders.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASample.NetCore.Services.Orders.Domain
{
    public class Order : AggregateRoot
    {
        public Guid CustomerId { get; private set; }
        public IEnumerable<OrderItem> Items { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; }
        public OrderStatus Status { get; private set; }

        public  Order(Guid id,Guid customerId, IEnumerable<OrderItem> items, string currency)
        {
            if (items == null || !items.Any())
            {
                throw new ASampleException("cannot_create_empty_order",
                    $"Cannot create an order for an empty cart for customer with id: '{customerId}'.");
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ASampleException("invalid_currency",
                    $"Cannot create an order with invalid currency for customer with id: '{customerId}'.");
            }
            Id = id;
            CustomerId = customerId;
            Items = items;
            Currency = currency;
            Status = OrderStatus.Created;
            TotalAmount = Items.Sum(i => i.TotalPrice);
        }

        public void Approve()
        {
            switch (Status)
            {
                case OrderStatus.Approved:
                    throw new ASampleException("cannot_approve_approved_order",
                        $"Cannot approve an approved order with id: '{Id}'.");
                case OrderStatus.Canceled:
                    throw new ASampleException("cannot_approve_canceled_order",
                        $"Cannot approve a canceled order with id: '{Id}'.");
                case OrderStatus.Revoked:
                    throw new ASampleException("cannot_approve_revoked_order",
                        $"Cannot approve a revoked order with id: '{Id}'.");
                case OrderStatus.Completed:
                    throw new ASampleException("cannot_approve_completed_order",
                        $"Cannot approve a completed order with id: '{Id}'.");
                default:
                    Status = OrderStatus.Approved;
                    break;
            }
        }

        public void Complete()
        {
            if (Status != OrderStatus.Approved)
            {
                throw new ASampleException("cannot_complete_not_approved_order",
                    $"Cannot complete not approved order with id: '{Id}'.");
            }

            switch (Status)
            {
                case OrderStatus.Canceled:
                    throw new ASampleException("cannot_complete_canceled_order",
                        $"Cannot complete a canceled order with id: '{Id}'.");
                case OrderStatus.Revoked:
                    throw new ASampleException("cannot_complete_revoked_order",
                        $"Cannot complete a revoked order with id: '{Id}'.");
                case OrderStatus.Completed:
                    throw new ASampleException("cannot_complete_completed_order",
                        $"Cannot complete an already completed order with id: '{Id}'.");
                default:
                    Status = OrderStatus.Completed;
                    break;
            }
        }

        public void Cancel()
        {
            switch (Status)
            {
                case OrderStatus.Canceled:
                    throw new ASampleException("cannot_cancel_canceled_order",
                        $"Cannot cancel an already canceled order with id: '{Id}'");
                case OrderStatus.Revoked:
                    throw new ASampleException("cannot_cancel_revoked_order",
                        $"Cannot cancel a revoked order with id: '{Id}'.");
                case OrderStatus.Completed:
                    throw new ASampleException("cannot_cancel_completed_order",
                        $"Cannot cancel a completed order with id: '{Id}'");
                default:
                    Status = OrderStatus.Canceled;
                    break;
            }
        }
        public void Revoke()
        {
            switch (Status)
            {
                case OrderStatus.Revoked:
                    throw new ASampleException("cannot_revoke_revoked_order",
                        $"Cannot revoke an already revoked order with id: '{Id}'");
                default:
                    Status = OrderStatus.Revoked;
                    break;
            }
        }
    }
}
