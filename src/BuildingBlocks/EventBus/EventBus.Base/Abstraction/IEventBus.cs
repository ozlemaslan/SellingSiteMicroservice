﻿using EventBus.Base.Events;
using System.Threading.Tasks;

namespace EventBus.Base.Abstraction
{
    /// <summary>
    /// Hangi eventi subscribe edeceklerini söyleyen interface
    /// </summary>
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}
