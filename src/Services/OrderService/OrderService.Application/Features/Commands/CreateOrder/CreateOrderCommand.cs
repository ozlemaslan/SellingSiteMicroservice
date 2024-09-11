using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public CreateOrderCommand(List<BasketItem> basketItems, string city, string street, string state, string country, string zipCode, string cardNumber, string cardHolderName, DateTime cardExpiration, string cardSecurityNumber, int cardTypeId, string userName)
        {

            var dtoList = basketItems.Select(item => new OrderItemDto()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            });

            orderItems = dtoList.ToList();

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
            UserName = userName;
        }

        public CreateOrderCommand()
        {
            orderItems=new List<OrderItemDto>();
        }

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
        public string UserName { get; set; }
        private List<OrderItemDto> orderItems;
        public List<OrderItemDto> OrderItems =>orderItems;

    }
}
