using MapsterMapper;
using SentimentAnalyser.Application.Common.Exceptions;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Commands.Update
{
    public class UpdateSentimentCommand : IRequestWrapper<SentimentDto>
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public float SentimentScore { get; set; }
    }

    public class UpdateSentimentCommandHandler : IRequestHandlerWrapper<UpdateSentimentCommand, SentimentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSentimentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SentimentDto>> Handle(UpdateSentimentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Sentiments.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Sentiment), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Word))
            {
                entity.Word = request.Word;
            }

            entity.SentimentScore = request.SentimentScore;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SentimentDto>(entity));
        }
    }
}