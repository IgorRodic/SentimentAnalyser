using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalyser.Application.Common.Models;
using SentimentAnalyser.Application.Dto;
using SentimentAnalyser.Application.Sentiments.Commands.Create;
using SentimentAnalyser.Application.Sentiments.Commands.Delete;
using SentimentAnalyser.Application.Sentiments.Commands.Update;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentimentById;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentiments;
using SentimentAnalyser.Application.Sentiments.Queries.GetSentimentScore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentAnalyser.WebApi.Controllers
{
    [ApiController]
    [EnableCors("allowSpecificOrigins")]
    [Route("[controller]")]
    public class SentimentController : BaseApiController
    {
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

        [HttpPost]
        public async Task<ActionResult<ServiceResult<SentimentDto>>> Create(CreateSentimentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<SentimentDto>>> Update(UpdateSentimentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<SentimentDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSentimentCommand() { Id = id }));
        }

        [HttpPost("calculation")]
        public async Task<ActionResult<ServiceResult<sbyte>>> CalculateSentimentScore(CalculateSentimentScoreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}