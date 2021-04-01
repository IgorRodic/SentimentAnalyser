using MediatR;
using SentimentAnalyser.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalyser.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}
