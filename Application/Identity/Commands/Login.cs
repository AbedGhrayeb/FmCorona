using Application.Common.Errors;
using Application.Interfaces;
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
    public class Login
    {
        public class Command : IRequest<TokenResponse>
        {
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, TokenResponse>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                IJwtGenerator jwtGenerator, IMapper mapper)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _mapper = mapper;
            }
            public async Task<TokenResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "خطأ في اسم المستخدم أو كلمة المرور" });
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                {
                    var token = _jwtGenerator.CreateToken(user);
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = "Proplem to generate token" });

                    }
                    var userDto = _mapper.Map<UserDto>(user);
                    return new TokenResponse(token, userDto);
                }
                throw new RestException(HttpStatusCode.BadRequest, new { msg = "خطأ في اسم المستخدم أو كلمة المرور" });

            }
        }
    }


}
