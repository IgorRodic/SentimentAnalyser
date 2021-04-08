using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Queries.GetSentimentById
{
    public class GetSentimentByIdQuery : IRequestWrapper<SentimentDto>
    {
        public int Id { get; set; }
    }

    public class GetSentimentByIdQueryHandler : IRequestHandlerWrapper<GetSentimentByIdQuery, SentimentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSentimentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SentimentDto>> Handle(GetSentimentByIdQuery request, CancellationToken cancellationToken)
        {
            var sentiment = await _context.Sentiments
                .Where(x => x.Id == request.Id)
                .ProjectToType<SentimentDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return sentiment != null ? ServiceResult.Success(sentiment) : ServiceResult.Failed<SentimentDto>(ServiceError.NotFount);
        }
    }
}
