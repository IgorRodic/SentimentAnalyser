using SentimentAnalyser.Domain.Common;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
