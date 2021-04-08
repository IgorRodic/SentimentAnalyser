using FluentAssertions;
using NUnit.Framework;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentiments;
using SentimentAnalyser.Domain.Entities;
using System.Threading.Tasks;
using static SentimentAnalyser.Application.IntegrationTests.TestingFixture;

namespace SentimentAnalyser.Application.IntegrationTests.Sentiments.Queries
{
    class GetAllSentimentsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllSentiments()
        {
            await AddAsync(new Sentiment
            {
                Word = "nice",
                SentimentScore = 0.4f
            });

            await AddAsync(new Sentiment
            {
                Word = "excellent",
                SentimentScore = 0.8f
            });

            await AddAsync(new Sentiment
            {
                Word = "modest",
                SentimentScore = 0
            });

            var query = new GetAllSentimentsQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(3);
        }

        [Test]
        public async Task ShouldReturnTheCorrectSentiment()
        {
            await AddAsync(new Sentiment
            {
                Word = "nice",
                SentimentScore = 0.4f
            });

            var query = new GetAllSentimentsQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(1);
            result.Data[0].Word.Should().Be("nice");
            result.Data[0].SentimentScore.Should().Be(0.4f);
        }
    }
}
