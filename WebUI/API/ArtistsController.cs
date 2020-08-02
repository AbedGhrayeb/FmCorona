using Application.Artists;
using Application.Artists.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Application.Artists.Queries.ArtistsList;

namespace WebUI.API
{
    public class ArtistsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ArtistsEnvelope>> GetArtists()
        {
            return await Mediator.Send(new ArtistsList.ArtistsListQuery());
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("favorite")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> AddFavoriteArtists(AddFavoriteArtist.AddCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
