using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Commands
{
    public class EditUser
    {
        public class EditUserCommand : IRequest<UserDto>
        {
            public string FullName { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            public int? DateOfBirthDay { get; set; }
            public int? DateOfBirthMonth { get; set; }
            public int? DateOfBirthYear { get; set; }
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
        }
        public class Handler : IRequestHandler<EditUserCommand, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IMapper mapper, IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.Username);
                if (request.ParseDateOfBirth().HasValue)
                {
                    user.DateOfBirth = request.ParseDateOfBirth().Value;
                }
                user.FullName = request.FullName ?? user.FullName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    var userDto = _mapper.Map<UserDto>(user);
                    return userDto;
                }
                throw new Exception("Proplem Saving Change");
            }
        }
    }
}
