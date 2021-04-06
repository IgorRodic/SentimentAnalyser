using SentimentAnalyser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Sentiment> Sentiments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}