using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Records.Commmands
{
    public class UserRecords
    {
        public class RecordCommand : IRequest
        {
            [Required]
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<RecordCommand>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IFilesAccessor _filesAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor, IFilesAccessor filesAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(RecordCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.Username);
                var recod = new Record
                {
                    AppUser = user,
                    RecordUrl = _filesAccessor.UploadFile(request.File, "records")
                };
                _context.Records.Add(recod);
                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    return Unit.Value;
                }
                throw new Exception("Proplem saving changes");
            }
        }
    }
}
