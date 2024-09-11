using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Domain.DomainEvents;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string Desc { get; set; }
        public Guid? BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public Address Address { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        private readonly List<OrderItem> orderItems;


        public Order()
        {
            orderItems = new List<OrderItem>();
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;

        }
        public Order(string userName, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration, Guid? buyerId, Guid? paymentMethodId,Address addr)
        {
            BuyerId = buyerId;
            PaymentMethodId = paymentMethodId;
            Address = addr;
            

            AddOrderStartedDomainEvent(userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

        }

        private void AddOrderStartedDomainEvent(string userName, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            OrderStartedDomainEvent orderStartedDomainEvent = new OrderStartedDomainEvent(userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

            this.AddDomainEvent(orderStartedDomainEvent);
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, int units, string pictureUrl)
        {
            OrderItem orderItem = new OrderItem(productId, productName, unitPrice, units, pictureUrl);

            this.orderItems.Add(orderItem);
        }

        private void SetBuyerId(Guid? buyerId)
        {
            BuyerId = buyerId;
        }

        private void SetPaymentMethodId(Guid? paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }
    }

    public enum OrderStatus
    {
        Submitted = 1,
        Paid = 2,
        Shipped = 3,
        Cancelled = 4,
    }
}
