using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Interfaces;
using SentimentAnalyser.Application.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Queries.GetSentimentScore
{
    public class CalculateSentimentScoreCommand : IRequestWrapper<sbyte>
    {
        public string Text { get; set; }
    }

    public class GetSentimentScoreQueryHandler : IRequestHandlerWrapper<CalculateSentimentScoreCommand, sbyte>
    {
        private readonly IApplicationDbContext _context;

        private readonly char[] delimiters = new char[] { ' ', ',', '.', ':', ';', '!', '?', '\t', '\n' };

        public GetSentimentScoreQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<sbyte>> Handle(CalculateSentimentScoreCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, float> sentiments = await _context.Sentiments.ToDictionaryAsync(s => s.Word, s => s.SentimentScore);

            float sentimentScore = 0.0f;

            var words = request.Text.ToLower().Split(delimiters);

            for(int i = 0; i < words.Length; i++)
            {
                if (sentiments.ContainsKey(words[i]))
                {
                    sentimentScore += sentiments[words[i]];
                }
            }

            sbyte connotation = 0;

            if (sentimentScore > 0)
            {
                connotation = 1;
            }
            else if (sentimentScore < 0)
            {
                connotation = -1;
            }

            return ServiceResult.Success(connotation);
        }
    }
}
