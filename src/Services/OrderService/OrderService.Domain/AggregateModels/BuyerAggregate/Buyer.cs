using OrderService.Domain.DomainEvents;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.AggregateModels.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        private readonly List<PaymentMethod> paymentMethods;
        public IEnumerable<PaymentMethod> PaymentMethods => paymentMethods.AsReadOnly();

        public Buyer()
        {
            paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string name)
        { 
            Name = name;
        }

        private void AddPaymentMethodVerifyDomainEvent(Buyer buyer, PaymentMethod paymentMethod, Guid orderId)
        {
            PaymentMethodVerifyDomainEvent paymentMethodVerifyDomainEvent = new PaymentMethodVerifyDomainEvent(buyer, paymentMethod, orderId);

            this.AddDomainEvent(paymentMethodVerifyDomainEvent);
        }
    }
}
