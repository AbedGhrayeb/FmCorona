using Application.Common.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
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
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(IExternalLoginService externalLogin, UserManager<AppUser> userManager,
                 IJwtGenerator jwtGenerator, DataContext context, IMapper mapper)
            {
                _externalLogin = externalLogin;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
                _mapper = mapper;
            }
            public async Task<TokenResponse> Handle(Commands request, CancellationToken cancellationToken)
            {
                var user = new AppUser();
                if (request.Provider.Equals("google"))
                {
                    var userInfo = await _externalLogin.GetGoogleInfoasync(request.AccessToken);
                    if (userInfo == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Token" });

                    }

                    var externalUser = await _context.ExternalLogins.SingleOrDefaultAsync(x => x.ProviderId == userInfo.Sub);
                    if (externalUser == null)
                    {
                        if ((await _context.Users.SingleOrDefaultAsync(x =>x.Email == userInfo.Email)) != null)
                        {
                            throw new RestException(HttpStatusCode.BadRequest, new { msg = $"already user with email: {userInfo.Email}" });

                        }
                        user = _mapper.Map<AppUser>(userInfo);


                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            var externalLogin = new ExternalLogin
                            {
                                ProviderId = userInfo.Sub,
                                ProviderName = "Google",
                                User = user
                            };
                            _context.ExternalLogins.Add(externalLogin);
                            if (!(await _context.SaveChangesAsync() > 0))
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to save exterinal login" });

                            }
                            var token = _jwtGenerator.CreateToken(user);
                            if (string.IsNullOrEmpty(token))
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to generate token" });

                            }
                            var userDto = _mapper.Map<UserDto>(user);
                            userDto.ImgUrl = user.ImgUrl;
                            return new TokenResponse(token, userDto);
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { msg = error.Description });

                            }
                        }
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to save user" });
                    }
                    else
                    {
                        var userDto = _mapper.Map<UserDto>(externalUser.User);
                        userDto.ImgUrl = user.ImgUrl;
                        return new TokenResponse(_jwtGenerator.CreateToken(externalUser.User), userDto);
                    }
                }

                else if (request.Provider.Equals("facebook"))
                {
                    var userInfo = await _externalLogin.GetFacebookInfoasync(request.AccessToken);
                    if (userInfo == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Token" });

                    }

                    var externalUser = await _context.ExternalLogins.SingleOrDefaultAsync(x => x.ProviderId == userInfo.Id);
                    if (externalUser == null)
                    {
                        user = _mapper.Map<AppUser>(userInfo);

                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            var externalLogin = new ExternalLogin
                            {
                                ProviderId = userInfo.Id,
                                ProviderName = "Facebook",
                                User = user
                            };
                            _context.ExternalLogins.Add(externalLogin);
                            if (!(await _context.SaveChangesAsync() > 0))
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to save exterinal login" });

                            }
                            var token = _jwtGenerator.CreateToken(user);
                            if (string.IsNullOrEmpty(token))
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to generate token" });

                            }
                            var userDto = _mapper.Map<UserDto>(user);
                            userDto.ImgUrl = user.ImgUrl;
                            return new TokenResponse(token, userDto);
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                throw new RestException(HttpStatusCode.BadRequest, new { error = error.Description });

                            }
                        }
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to save user" });
                    }
                    else
                    {
                        var userDto = _mapper.Map<UserDto>(externalUser.User);
                        userDto.ImgUrl = user.ImgUrl;

                        return new TokenResponse(_jwtGenerator.CreateToken(externalUser.User), userDto);
                    }
                }
                else
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "Invalid Provider" });
                }


            }
        }
    }
}
