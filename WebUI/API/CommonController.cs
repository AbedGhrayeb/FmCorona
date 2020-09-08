using Application.Records.Commmands;
using Application.Topics.Commands;
using Application.Topics.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Application.Topics.Queries.TopicDetails;
using static Application.Topics.Queries.TopicsList;

namespace WebUI.API
{

    public class CommonController : BaseApiController
    {

        [HttpPost("contact_us")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> ContactUs(Contacts.ContactCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TopicsEnvelope>> GetTopics()
        {
            return await Mediator.Send(new TopicsList.TopicsListQuery());
        }
        [HttpGet("topics/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TopicEnvelope>> GetTopic(int id)
        {
            return await Mediator.Send(new TopicDetails.TopicDeatailsQuery { Id = id });
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("record")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> AddRecord([FromForm] UserRecords.RecordCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
