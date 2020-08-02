using Application.Identity;
using Application.Identity.Commands;
using Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<TokenResponse> Regester([FromForm]Register.RegisterCommand command)
        {
            return await Mediator.Send(command);
        } 
        
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<UserDto> EditProfile([FromForm]EditUser.EditUserCommand command)
        {
            return await Mediator.Send(command);
        } 
        [Consumes("application/json")]
        [HttpPost("change_password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<Unit> ChangePassword(ChangePassword.ChangePasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            return await Mediator.Send(new CurrentUser.CurrentUserQuery());
        }

    }
}
