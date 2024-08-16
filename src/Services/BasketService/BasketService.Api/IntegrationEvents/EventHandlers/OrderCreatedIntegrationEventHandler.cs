using BasketService.Api.Core.Application.Repository;
using BasketService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BasketService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly ILogger Logger;
        private readonly IBasketRepository _basketRepository;

        public OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger, IBasketRepository basketRepository)
        {
            Logger = logger;
            _basketRepository = basketRepository;
        }

        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            if (@event.CustomerBasket == null || @event.CustomerBasket.Items == null)
            {
                return null;
            }

            _basketRepository.DeleteBasketAsync(@event.Buyer);

            Logger.LogInformation($"OrderCreatedIntegrationEventHandler deleted from BasketService.");

            return Task.CompletedTask;
        }
    }
}
