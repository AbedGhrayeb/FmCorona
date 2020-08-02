using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Presenters.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Presenters.Queries.PresenterDetails;
using static Application.Presenters.Queries.PresentersList;

namespace WebUI.API
{

    public class PresentersController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PresentersEnvelope>> GetPresenters()
        {
            return await Mediator.Send(new PresentersList.PresentersListQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PresenterEnvelope>> GetEpisode(int id)
        {
            return await Mediator.Send(new PresenterDetails.PresenterDetailsQuery { Id = id });
        }
    }
}
