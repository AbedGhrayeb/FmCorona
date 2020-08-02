using Application.Articals.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Application.Articals.Queries.ArticalDetails;
using static Application.Articals.Queries.ArticalList;

namespace WebUI.API
{
    public class ArticalsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<HighlightsEnvelope>> GetArticls()
        {
            return await Mediator.Send(new ArticalList.ArticalListQuery()) ;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<HighlightEnvelope>> GetArtical(int id)
        {
            return await Mediator.Send(new ArticalDetails.ArticalQuery { Id = id });
        }
    }
}
