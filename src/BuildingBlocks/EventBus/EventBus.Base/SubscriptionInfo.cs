﻿using System;

namespace EventBus.Base
{
    /// <summary>
    /// Dışarıdan bize gönderilen integrationEventin tipinin burada tutulması için
    /// </summary>
    public class SubscriptionInfo
    {
        public Type HandlerType { get; set; }
        public SubscriptionInfo(Type handlerType)
        {
            HandlerType= handlerType ?? throw new ArgumentNullException(nameof(handlerType));
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
