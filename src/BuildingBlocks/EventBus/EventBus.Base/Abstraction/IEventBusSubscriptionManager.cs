using EventBus.Base.Events;
using System;
using System.Collections.Generic;

namespace EventBus.Base.Abstraction
{
    /// <summary>
    /// Dışarıdan bize gönderilen subcribeleri bir dictionary şeklinde tutacağız. Hem eventi hem handlerı barındıran dictionary bunu bu projede şimdilik inmemory üstünde tutuyoruz, bunu yarın bir gün db de de tutmak isteyebiliriz.
    /// Böyle bir durumda biz bu gibi durumları değştirebilelim diye bu manager servis var.
    /// </summary>
    public interface IEventBusSubscriptionManager
    {
        bool IsEmpty { get; }     //Herhangibir eventi dinliyor muyuz? 

        event EventHandler<string> OnEventRemoved;
        void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void RemoveSubscription<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
        bool HasSubscriptionForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionForEvent(string eventName);
        Type GetEventTypeByName(string eventName); // eventin kendisinden handleriına ulaş
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent; //eventin tüm Subscriptionları ve handlerlarını dönen metot
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
