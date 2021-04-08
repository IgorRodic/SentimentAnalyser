using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Commands.Create
{
    public class CreateSentimentCommandValidator : AbstractValidator<CreateSentimentCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSentimentCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Word)
                .MaximumLength(100).WithMessage("Word must not exceed 100 characters.")
                .MustAsync(BeUniqueWord).WithMessage("The specified word already exists.")
                .NotEmpty().WithMessage("Word is required.");
        }

        private async Task<bool> BeUniqueWord(string word, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Sentiments.AllAsync(x => x.Word != word, cancellationToken);
        }
    }
}