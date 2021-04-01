namespace SentimentAnalyser.Domain.Entities
{
    public class Sentiment
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public float SentimentScore { get; set; }

        public Sentiment() { }

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
