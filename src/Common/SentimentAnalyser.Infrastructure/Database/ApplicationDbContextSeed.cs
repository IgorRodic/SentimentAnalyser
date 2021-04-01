using Microsoft.AspNetCore.Identity;
using SentimentAnalyser.Domain.Entities;
using SentimentAnalyser.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Infrastructure.Database
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var defaultUser = new ApplicationUser { UserName = "iayti", Email = "test@test.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Matech_1850");
                await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleCityDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
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

                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
