using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using MapsterMapper;
using SentimentAnalyser.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SentimentAnalyser.Application.Sentiments.Queries.GetSentiments
{
    public class GetAllSentimentsQuery : IRequestWrapper<List<SentimentDto>>
    {
    }

    public class GetSentimentsQueryHandler : IRequestHandlerWrapper<GetAllSentimentsQuery, List<SentimentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSentimentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<SentimentDto>>> Handle(GetAllSentimentsQuery request, CancellationToken cancellationToken)
        {
            List<SentimentDto> list = await _context.Sentiments
                .ProjectToType<SentimentDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<SentimentDto>>(ServiceError.NotFount);
        }
    }
}
