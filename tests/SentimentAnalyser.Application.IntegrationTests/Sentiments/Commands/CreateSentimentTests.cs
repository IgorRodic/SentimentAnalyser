using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using SentimentAnalyser.Application.Sentiments.Commands.Create;
using SentimentAnalyser.Domain.Entities;
using System.Threading.Tasks;
using static SentimentAnalyser.Application.IntegrationTests.TestingFixture;

namespace SentimentAnalyser.Application.IntegrationTests.Sentiments.Commands
{
    class CreateSentimentTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateSentimentCommand();

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueWord()
        {
            await SendAsync(new CreateSentimentCommand
            {
                Word = "nice",
                SentimentScore = 0.4f
            });

            var command = new CreateSentimentCommand
            {
                Word = "nice",
                SentimentScore = 0.4f
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateSentiment()
        {
            var command = new CreateSentimentCommand
            {
                Word = "nice",
                SentimentScore = 0.4f
            };

            var result = await SendAsync(command);

            var list = await FindAsync<Sentiment>(result.Data.Id);

            list.Should().NotBeNull();
            list.Word.Should().Be(command.Word);
            list.SentimentScore.Should().Be(command.SentimentScore);
        }
    }
}
