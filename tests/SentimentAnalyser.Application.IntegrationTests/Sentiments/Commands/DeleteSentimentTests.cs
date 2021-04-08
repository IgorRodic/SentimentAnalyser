using FluentAssertions;
using NUnit.Framework;
using SentimentAnalyser.Application.Common.Exceptions;
using SentimentAnalyser.Application.Sentiments.Commands.Create;
using SentimentAnalyser.Application.Sentiments.Commands.Delete;
using SentimentAnalyser.Application.Sentiments.Commands.Update;
using SentimentAnalyser.Domain.Entities;
using System.Threading.Tasks;
using static SentimentAnalyser.Application.IntegrationTests.TestingFixture;

namespace SentimentAnalyser.Application.IntegrationTests.Sentiments.Commands
{
    class DeleteSentimentTests : TestBase
    {
        [Test]
        public void ShouldRequireValidSentimentId()
        {
            var command = new DeleteSentimentCommand { Id = 99 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteSentiment()
        {
            var sentiment = await SendAsync(new CreateSentimentCommand
            {
                Word = "nice",
                SentimentScore = 0.4f
            });

            await SendAsync(new DeleteSentimentCommand
            {
                Id = sentiment.Data.Id
            });

            var list = await FindAsync<Sentiment>(sentiment.Data.Id);

            list.Should().BeNull();
        }
    }
}
