using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Domain.Common;
using SentimentAnalyser.Domain.Entities;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }

        public DbSet<Sentiment> Sentiments { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}