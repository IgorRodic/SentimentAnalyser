using MapsterMapper;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Domain.Entities;
using SentimentAnalyser.Domain.Event;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Commands.Create
{
    public class CreateSentimentCommand : IRequestWrapper<SentimentDto>
    {
        public string Word { get; set; }

        public float SentimentScore { get; set; }
    }

    public class CreateSentimentCommandHandler : IRequestHandlerWrapper<CreateSentimentCommand, SentimentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSentimentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SentimentDto>> Handle(CreateSentimentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Sentiment
            {
                Word = request.Word,
                SentimentScore = request.SentimentScore
            };

            entity.DomainEvents.Add(new SentimentCreateEvent(entity));

            await _context.Sentiments.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SentimentDto>(entity));
        }
    }
}