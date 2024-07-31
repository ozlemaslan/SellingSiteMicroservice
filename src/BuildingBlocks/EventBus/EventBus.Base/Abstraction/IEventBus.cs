using EventBus.Base.Events;

namespace EventBus.Base.Abstraction
{
    /// <summary>
    /// Hangi eventi subscribe edeceklerini söyleyen interface
    /// </summary>
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event); // servisimiz event fırlatacağı zaman bu metodu çağıracak.
        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}
