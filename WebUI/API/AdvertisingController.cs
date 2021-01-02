using Application.Advertisings;
using Application.Advertisings.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.API
{

    public class AdvertisingController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IReadOnlyList<AdvertisingVm>> GetAdvertisings()
        {
            return await Mediator.Send(new AdvertisingsList.AdvertisingsQuery());
        }
    }
}
