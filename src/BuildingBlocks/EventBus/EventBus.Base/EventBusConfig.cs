using System;

namespace EventBus.Base
{
    /// <summary>
    /// Herhangi bir constr olacak mı veya dışarıdan kaç kere rabbitmq ya bağlanılacak bu gibi durumlar için bir config oluşturduk.
    /// </summary>
    public class EventBusConfig
    {
        public int ConnectionRetryCount { get; set; } = 5; //Rabbitmq ya bağlanırken en fazla 5 kere dene
        public string DefaultTopicName { get; set; } = "SellingSiteEventBus";
        public string EventBusConnectionString { get; set; } = String.Empty;
        public string SubscriberClientAppName { get; set; } = String.Empty; //Queueları oluşturmak için eventlerin başına hangi servis hangi eventi dinliyor ise diye yazıldı.Mesela Order servisinden OrderCreated eventi dinleniyor. Order.OrderCreated=queuename
        public string EventNamePrefix { get; set; } = String.Empty;
        public string EventNameSuffix { get; set; } = "IntegrationEvent"; //OrderCreatedIntegrationEvent yerine trim yaparak OrderCreated olacak hale getirir.
        public EventBusType EventBusType { get; set; } = EventBusType.RabbitMQ;
        public object Connection { get; set; }
        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);
        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);
    }

    public enum EventBusType
    {
        RabbitMQ = 0,
        AzureServiceBus = 1
    }
}
