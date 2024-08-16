using BasketService.Api.Core.Domain;
using EventBus.Base.Events;
using System;

namespace BasketService.Api.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent :IntegrationEvent
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public DateTime CardExpiration { get; set; }

        public string CardSecurityNumber { get; set; }

        public int CardTypeId { get; set; }

        public string Buyer { get; set; }// kullanıcı idsi gibi yani satın alan kişi
        public CustomerBasket CustomerBasket { get; set; }//oluşturulan sepet bilgileri

        public OrderCreatedIntegrationEvent()
        {
            
        }

        public OrderCreatedIntegrationEvent(string city, string street, string state, string country, string zipCode, string cardNumber, string cardHolderName, DateTime cardExpiration, string cardSecurityNumber, int cardTypeId, string buyer, CustomerBasket basket)
        {
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            Buyer = buyer;
            CustomerBasket = basket;
        }
    }
}
