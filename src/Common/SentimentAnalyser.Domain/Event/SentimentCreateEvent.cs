using System;
using System.Collections.Generic;
using System.Text;
using SentimentAnalyser.Domain.Common;
using SentimentAnalyser.Domain.Entities;

namespace SentimentAnalyser.Domain.Event
{
    public class SentimentCreateEvent : DomainEvent
    {
        public SentimentCreateEvent(Sentiment sentiment)
        {
            Sentiment = sentiment;
        }

        public Sentiment Sentiment { get; }
    }
}