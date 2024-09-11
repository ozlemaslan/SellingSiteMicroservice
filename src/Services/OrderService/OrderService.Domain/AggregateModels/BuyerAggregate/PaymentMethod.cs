using OrderService.Domain.SeedWork;
using System;

namespace OrderService.Domain.AggregateModels.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public PaymentMethod(string cardNumber, string cardName, string cvv, DateTime expDate, CardType cardType)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            Cvv = cvv;
            ExpDate = expDate;
            CardType = cardType;
        }

        public PaymentMethod()
        {
            
        }
         
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpDate { get; set; }
        public CardType CardType { get; set; }
    }

    public enum CardType
    {
        Amex=1,
        Visa=2,
        Mastercard=3
    }
}
