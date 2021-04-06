using SentimentAnalyser.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentAnalyser.Infrastructure.Database
{
    public static class ApplicationDbContextSeed
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            if (!context.Sentiments.Any())
            {
                context.Sentiments.AddRange(new Sentiment
                {
                    Id = 1,
                    Word = "nice",
                    SentimentScore = 0.4f,
                }, new Sentiment
                {
                    Id = 2,
                    Word = "excellent",
                    SentimentScore = 0.8f,
                }, new Sentiment
                {
                    Id = 3,
                    Word = "modest",
                    SentimentScore = 0f,
                }, new Sentiment
                {
                    Id = 4,
                    Word = "horrible",
                    SentimentScore = -0.8f,
                }, new Sentiment
                {
                    Id = 5,
                    Word = "ugly",
                    SentimentScore = -0.5f,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}