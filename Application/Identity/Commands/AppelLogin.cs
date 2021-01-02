using Application.Common.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class AppelLogin
    {
        public class AppelLoginCommand : IRequest<TokenResponse>
        {
            public string AppelId { get; set; }
            [Required]
            public string Name { get; set; }
            [DataType(DataType.EmailAddress)]
            [Required]
            public string Email { get; set; }


        }
        public class Handler : IRequestHandler<AppelLoginCommand, TokenResponse>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;


            public Handler(UserManager<AppUser> userManager, IMapper mapper, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;

            }
            public async Task<TokenResponse> Handle(AppelLoginCommand request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByNameAsync(request.AppelId);
                if (user == null)
                {
                    var newUser = new AppUser
                    {
                        UserName = request.AppelId,
                        Email = request.Email,
                        FullName = request.Name,

                    };

                    var result = await _userManager.CreateAsync(newUser);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "user");
                        var token = _jwtGenerator.CreateToken(newUser);
                        if (string.IsNullOrEmpty(token))
                        {
                            throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to generate token" });

                        }
                        var userDto = _mapper.Map<UserDto>(newUser);
                        return new TokenResponse(token, userDto);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            throw new RestException(HttpStatusCode.BadRequest, new { msg = error.Description });

                        }
                    }
                }

                else
                {
                    var token = _jwtGenerator.CreateToken(user);
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to generate token" });

                    }
                    var userDto = _mapper.Map<UserDto>(user);
                    return new TokenResponse(token, userDto);
                }

                throw new Exception("Proplem Saving Change");
            }

        }


    }
}

