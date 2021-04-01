using Microsoft.AspNetCore.Http;
using SentimentAnalyser.Application.Common.Interfaces;
using System.Security.Claims;

namespace SentimentAnalyser.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
