using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentimentById;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentiments;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SentimentController : BaseApiController
    {

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<SentimentDto>>>> GetAllSentiments(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllSentimentsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<SentimentDto>>> GetSentimentById(int id)
        {
            return Ok(await Mediator.Send(new GetSentimentByIdQuery { Id = id }));
        }

        //[HttpPost]
        //public async Task<ActionResult<ServiceResult<SentimentDto>>> Create(CreateCityCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        //[HttpPut]
        //public async Task<ActionResult<ServiceResult<SentimentDto>>> Update(UpdateCityCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ServiceResult<SentimentDto>>> Delete(int id)
        //{
        //    return Ok(await Mediator.Send(new DeleteCityCommand { Id = id }));
        //}
    }
}
