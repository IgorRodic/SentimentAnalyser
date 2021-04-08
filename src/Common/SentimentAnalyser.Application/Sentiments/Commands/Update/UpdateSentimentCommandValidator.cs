using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.Sentiments.Commands.Update
{
    public class UpdateSentimentCommandValidator : AbstractValidator<UpdateSentimentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSentimentCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Word)
                .MaximumLength(100).WithMessage("Word must not exceed 100 characters.")
                .WithMessage("The specified word already exists.");

            RuleFor(v => v.Id).NotNull();
        }
    }
}