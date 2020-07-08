using Application.Identity;
using Application.Identity.Commands;
using Application.Identity.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.API
{
    public class UsersController : BaseApiController
    {
        [AllowAnonymous]
        [Consumes("application/json")]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<TokenResponse> Login(Login.Command command)
        {
            return await Mediator.Send(command);     
        }

        [AllowAnonymous]
        [Consumes("application/json")]
        [HttpPost("external")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<TokenResponse> ExternalLogin(External.Commands command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            return await Mediator.Send(new Profile.GetProfile());
        }

    }
}
