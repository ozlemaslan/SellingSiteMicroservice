using Newtonsoft.Json;
using System;

namespace EventBus.Base.Events
{
    /// <summary>
    /// Servisler arası event için yazıldı. Yani rabbitMq vs ile diğer servislerin haberleşmesi için oluşturulan event
    /// </summary>
    public class IntegrationEvent
    {
        public Guid Id { get;private set; }

        public DateTime CreatedDate { get;private set; }

        public IntegrationEvent()
        {
            Id= Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        [JsonConstructor] //json kullanılarak bir serilization işlemi yapıldığında diyelim ki buradan alarak dışardan gelen parametreleri set etmek için yazıldı.
        public IntegrationEvent(Guid id, DateTime createdDate)
        {
            Id= id;
            CreatedDate= createdDate;
        }
    }
}
