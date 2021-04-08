using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentimentAnalyser.Domain.Entities;

namespace SentimentAnalyser.Infrastructure.Database.Configurations
{
    public class SentimentConfiguration : IEntityTypeConfiguration<Sentiment>
    {
        public void Configure(EntityTypeBuilder<Sentiment> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Id).HasColumnName(nameof(Sentiment.Id)).IsRequired();
            builder.Property(t => t.Word).HasColumnName(nameof(Sentiment.Word)).HasMaxLength(100).IsRequired();
            builder.Property(t => t.SentimentScore).HasColumnName(nameof(Sentiment.SentimentScore)).IsRequired();
        }
    }
}