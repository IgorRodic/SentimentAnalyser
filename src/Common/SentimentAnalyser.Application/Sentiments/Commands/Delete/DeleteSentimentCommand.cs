using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Exceptions;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Domain.Entities;

namespace SentimentAnalyser.Application.Sentiments.Commands.Delete
{
    public class DeleteSentimentCommand : IRequestWrapper<SentimentDto>
    {
        public int Id { get; set; }
    }

    public class DeleteSentimentCommandHandler : IRequestHandlerWrapper<DeleteSentimentCommand, SentimentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteSentimentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SentimentDto>> Handle(DeleteSentimentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Sentiments
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Sentiment), request.Id);
            }

            _context.Sentiments.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SentimentDto>(entity));
        }
    }
}