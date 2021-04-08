using System;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Domain.Entities;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace SentimentAnalyser.Application.UnitTests
{
    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
        }


        [Test]
        [TestCase(typeof(Sentiment), typeof(SentimentDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }

        [Test]
        public void ShouldMapSentimentToSentimentDtoCorrectly()
        {
            var sentiment = new Sentiment { Id = 1, Word = "nice", SentimentScore = 0.4f };
            var sentimentDto = _mapper.Map<Sentiment, SentimentDto>(sentiment);

            sentimentDto.Word.Should().Be("nice");
            sentimentDto.SentimentScore.Should().Be(0.4f);
        }
    }
}
