using Application.Common.Errors;
using Application.Interfaces;
using Application.Interfaces.ExternalAuth;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class External
    {
        public class Commands : IRequest<TokenResponse>
        {
            [Required]
            public string Provider { get; set; }
            [Required]
            public string AccessToken { get; set; }
        }
        public class Handler : IRequestHandler<Commands, TokenResponse>
        {
            private readonly IExternalLoginService _externalLogin;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _mapper;

            public Handler(IExternalLoginService externalLogin, UserManager<AppUser> userManager,
                 IJwtGenerator jwtGenerator, IMapper mapper)
            {
                _externalLogin = externalLogin;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _mapper = mapper;
            }
            public async Task<TokenResponse> Handle(Commands request, CancellationToken cancellationToken)
            {
                var user = new AppUser();
                var googleUser = new GoogleUserInfo();
                var facebookUser = new FacebookUserInfoResult();
                if (request.Provider.Equals("google"))
                {
                    googleUser = await _externalLogin.GetGoogleInfoasync(request.AccessToken);
                    if (googleUser == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Token" });
                    }
                    user = _mapper.Map<AppUser>(googleUser);
                }
                else if (request.Provider.Equals("facebook"))
                {
                    facebookUser = await _externalLogin.GetFacebookInfoasync(request.AccessToken);
                    if (facebookUser == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Token" });
                    }
                    user = _mapper.Map<AppUser>(facebookUser);
                }
                else
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Provider" });
                }
                var newUser = await _userManager.FindByIdAsync(user.Id);
                if (newUser == null)
                {
                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "user");

                        var newDto = _mapper.Map<UserDto>(user);
                        newDto.ImgUrl = user.ImgUrl;
                        return new TokenResponse(_jwtGenerator.CreateToken(user), newDto);
                    }
                }

                var userDto = _mapper.Map<UserDto>(user);
                userDto.ImgUrl = user.ImgUrl;
                return new TokenResponse(_jwtGenerator.CreateToken(user), userDto);
            }
        }

    }
}

