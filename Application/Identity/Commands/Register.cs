using Application.Common.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class Register
    {
        public class RegisterCommand : IRequest<TokenResponse>
        {
            [Required]
            public string FullName { get; set; }
            [DataType(DataType.EmailAddress)]
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            public int DateOfBirthDay { get; set; }
            public int DateOfBirthMonth { get; set; }
            public int DateOfBirthYear { get; set; }
            public DateTime ParseDateOfBirth()
            {

                return new DateTime(DateOfBirthYear, DateOfBirthMonth, DateOfBirthDay);

            }
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            [Required]
            public string ConfirmPassword { get; set; }
        }
        public class Handler : IRequestHandler<RegisterCommand, TokenResponse>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IMapper mapper, IJwtGenerator jwtGenerator,
                IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
                _userAccessor = userAccessor;
            }
            public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName == _userAccessor.Username))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "You are already registered, logout to new register" });
                }
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Email))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "This Email Already Token" });
                }

                var user = new AppUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    DateOfBirth = request.ParseDateOfBirth(),
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, request.Password);
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
                throw new Exception("Proplem Saving Change");
            }
        }
    }
}
