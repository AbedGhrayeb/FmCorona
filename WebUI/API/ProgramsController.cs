using Application.Programs;
using Application.Programs.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Application.Programs.Queries.EpisodesList;
using static Application.Programs.Queries.ProgramsList;
using static Application.Programs.Queries.WeeklySchedule;

namespace WebUI.API
{
    public class ProgramsController : BaseApiController
    {


        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProgramsEnvelope>> GetPrograms()
        {
            return await Mediator.Send(new ProgramsList.ProgramsListQuery());
        }

        [HttpGet("{program_id}/episodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EpisodesEnvelope>> GetEpisodes(int program_id,int? page,int? limit,string title)
        {
            return await Mediator.Send(new EpisodesList.EpisodesListQuery(page,limit,title) { ProgramId = program_id });
        }

        [HttpGet("episodes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EpisodeDto>> GetEpisode(int id)
        {
            return await Mediator.Send(new EpisodeDetails.EpisodeDetailsQuery { Id = id });
        }

        [HttpGet("schedules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ScheduleEnvelope>> Getschedules(int? dayOfWeek)
        {
            return await Mediator.Send(new WeeklySchedule.ScheduleQuery(dayOfWeek));
        }
    }
}
