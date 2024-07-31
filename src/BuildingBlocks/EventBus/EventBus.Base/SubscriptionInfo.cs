using System;

namespace EventBus.Base
{
    /// <summary>
    /// Dışarıdan bize gönderilen integrationEventin tipinden hangi handler classı olacağını bulmak için yapıldı.
    /// </summary>
    public class SubscriptionInfo
    {
        public Type HandlerType { get; set; }
        public SubscriptionInfo(Type handlerType)
        {
            HandlerType= handlerType ?? throw new ArgumentNullException(nameof(handlerType));
        }

        /// <summary>
        /// IntegrationEventin tipinin tutulmasını sağlar.
        /// </summary>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
