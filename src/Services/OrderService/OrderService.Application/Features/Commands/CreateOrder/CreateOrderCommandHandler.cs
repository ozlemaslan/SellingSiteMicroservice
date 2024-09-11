using EventBus.Base.Abstraction;
using MediatR;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Repositories.Interfaces;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Domain.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address= new Address(request.Street,request.City,request.Country,request.State,request.ZipCode);

            var order = new Order(request.UserName,request.CardTypeId,request.CardNumber,request.CardSecurityNumber,request.CardHolderName,request.CardExpiration,Guid.NewGuid(),Guid.NewGuid(),address);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Units, item.PictureUrl);
            }

            _orderRepository.AddAsync(order);
            _unitOfWork.SaveChangesAsync(cancellationToken);

            OrderStartedIntegrationEvent orderStartedIntegrationEvent = new OrderStartedIntegrationEvent();

            _eventBus.Publish(orderStartedIntegrationEvent);


            return Task.FromResult(true);
        }
    }
}
