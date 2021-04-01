﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentimentAnalyser.Domain.Entities;

namespace SentimentAnalyser.Infrastructure.Database.Configurations
{
    public class SentimentConfiguration : IEntityTypeConfiguration<Sentiment>
    {
        public void Configure(EntityTypeBuilder<Sentiment> builder)
        {
            builder.Property(t => t.Id).HasColumnName(nameof(Sentiment.Id)).IsRequired();
            builder.Property(t => t.Word).HasColumnName(nameof(Sentiment.Word)).HasMaxLength(50).IsRequired();
            builder.Property(t => t.SentimentScore).HasColumnName(nameof(Sentiment.SentimentScore)).IsRequired();
        }
    }
}