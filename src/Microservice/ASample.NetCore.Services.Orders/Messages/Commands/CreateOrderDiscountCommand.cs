using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Commands
{
    /// <summary>
    /// 订单折扣
    /// </summary>
    public class CreateOrderDiscountCommand : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public int Percentage { get; }

        [JsonConstructor]
        public CreateOrderDiscountCommand(Guid id, Guid customerId, int percentage)
        {
            Id = id;
            CustomerId = customerId;
            Percentage = percentage;
        }
    }
}
