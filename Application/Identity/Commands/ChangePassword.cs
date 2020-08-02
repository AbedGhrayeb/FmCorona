using Application.Common.Errors;
using Application.Interfaces;
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
    public class ChangePassword
    {
        public class ChangePasswordCommand : IRequest
        {
            [Required]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(NewPassword))]
            public string ConfirmPassword { get; set; }
        }
        public class Handler : IRequestHandler<ChangePasswordCommand>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IUserAccessor userAccessor)
            {
                _userManager = userManager;

                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.Username);


                var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    return Unit.Value;
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