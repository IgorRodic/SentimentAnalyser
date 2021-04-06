using System.Collections.Generic;
using SentimentAnalyser.Domain.Common;

namespace SentimentAnalyser.Domain.Entities
{
    public class Sentiment
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public float SentimentScore { get; set; }

        public List<DomainEvent> DomainEvents { get; set; }

        public Sentiment()
        {
            DomainEvents = new List<DomainEvent>();
        }

        public Sentiment(int id, string word, float sentimentScore)
        {
            Id = id;
            Word = word;
            SentimentScore = sentimentScore;
        }

        public Sentiment(string word, float sentimentScore)
        {
            Word = word;
            SentimentScore = sentimentScore;
        }
    }
}