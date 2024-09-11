using MediatR;
using OrderService.Domain.AggregateModels.BuyerAggregate;
using System;

namespace OrderService.Domain.DomainEvents
{
    public class PaymentMethodVerifyDomainEvent: INotification
    {
        public PaymentMethodVerifyDomainEvent(Buyer buyer, PaymentMethod paymentMethod, Guid orderId)
        {
            Buyer = buyer;
            PaymentMethod = paymentMethod;
            OrderId = orderId;
        }

        public Buyer Buyer { get; set; }       
        public PaymentMethod PaymentMethod { get; set; }
        public Guid OrderId { get; set; }
    }
}
