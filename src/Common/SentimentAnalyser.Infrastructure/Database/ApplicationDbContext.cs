using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Infrastructure.Identity;
using SentimentAnalyser.Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using SentimentAnalyser.Domain.Common;

namespace SentimentAnalyser.Infrastructure.Database
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime,
            IDomainEventService domainEventService) : base(options, operationalStoreOptions)
        {
            _dateTime = dateTime;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
        }

        public DbSet<Sentiment> Sentiments { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Creator = _currentUserService.UserId;
                        entry.Entity.CreateDate = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modifier = _currentUserService.UserId;
                        entry.Entity.ModifyDate = _dateTime.Now;
                        break;
                }
            }

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
