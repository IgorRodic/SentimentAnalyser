﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SentimentAnalyser.Infrastructure.Database;

namespace SentimentAnalyser.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210403203141_SentimentDb-v2")]
    partial class SentimentDbv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SentimentAnalyser.Domain.Entities.Sentiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("SentimentScore")
                        .HasColumnType("real")
                        .HasColumnName("SentimentScore");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Word");

                    b.HasKey("Id");

                    b.ToTable("Sentiments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SentimentScore = 0.4f,
                            Word = "nice"
                        },
                        new
                        {
                            Id = 2,
                            SentimentScore = 0.8f,
                            Word = "excellent"
                        },
                        new
                        {
                            Id = 3,
                            SentimentScore = 0f,
                            Word = "modest"
                        },
                        new
                        {
                            Id = 4,
                            SentimentScore = -0.8f,
                            Word = "horrible"
                        },
                        new
                        {
                            Id = 5,
                            SentimentScore = -0.5f,
                            Word = "ugly"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}