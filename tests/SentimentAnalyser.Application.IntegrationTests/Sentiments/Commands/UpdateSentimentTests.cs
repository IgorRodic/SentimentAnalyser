using FluentAssertions;
using NUnit.Framework;
using SentimentAnalyser.Application.Common.Exceptions;
using SentimentAnalyser.Application.Sentiments.Commands.Create;
using SentimentAnalyser.Application.Sentiments.Commands.Update;
using SentimentAnalyser.Domain.Entities;
using System.Threading.Tasks;
using static SentimentAnalyser.Application.IntegrationTests.TestingFixture;


namespace SentimentAnalyser.Application.IntegrationTests.Sentiments.Commands
{
    class UpdateSentimentTests : TestBase
    {
        [Test]
        public void ShouldRequireValidSentimentId()
        {
            var command = new UpdateSentimentCommand
            {
                Id = 99,
                Word = "test"
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateSentiment()
        {
            var result = await SendAsync(new CreateSentimentCommand
            {
                Word = "nice",
                SentimentScore = 0.4f
            });

            var command = new UpdateSentimentCommand
            {
                Id = result.Data.Id,
                Word = "nice",
                SentimentScore = 0.8f
            };

            await SendAsync(command);

            var sentiment = await FindAsync<Sentiment>(result.Data.Id);

            sentiment.Word.Should().Be(command.Word);
            sentiment.SentimentScore.Should().Be(command.SentimentScore);
        }
    }
}
