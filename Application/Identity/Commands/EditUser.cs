using Application.Common.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class EditUser
    {
        public class EditUserCommand : IRequest<UserDto>
        {
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            public string FullName { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            public int? DateOfBirthDay { get; set; }
            public int? DateOfBirthMonth { get; set; }
            public int? DateOfBirthYear { get; set; }
            [DataType(DataType.Date)]
            public DateTime? ParseDateOfBirth()
            {
                if (!DateOfBirthYear.HasValue || !DateOfBirthMonth.HasValue || !DateOfBirthDay.HasValue)
                    return null;

                DateTime? dateOfBirth = null;
                try
                {
                    dateOfBirth = new DateTime(DateOfBirthYear.Value, DateOfBirthMonth.Value, DateOfBirthDay.Value);
                }
                catch { }
                return dateOfBirth;
            }
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<EditUserCommand, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly IFilesAccessor _filesAccessor;

            public Handler(UserManager<AppUser> userManager, IMapper mapper, IUserAccessor userAccessor, IFilesAccessor filesAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _filesAccessor = filesAccessor;
                _userAccessor = userAccessor;
            }
            public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.Username);
                if (request.ParseDateOfBirth().HasValue)
                {
                    user.DateOfBirth = request.ParseDateOfBirth().Value;
                }
                var emailExist = await _userManager.FindByEmailAsync(request.Email);
                if (emailExist!=null && emailExist.Id !=user.Id)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = $"already user with email: {request.Email}" });

                }
                user.Email = request.Email ?? user.Email;
                user.FullName = request.FullName ?? user.FullName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.ImgUrl = _filesAccessor.ChangeFile(request.File, user.ImgUrl) ?? user.ImgUrl;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    var userDto = _mapper.Map<UserDto>(user);
                    return userDto;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new { msg = error.Description });

                    }
                }
                throw new Exception("Proplem Saving Change");
            }
        }
    }
}
