using Application.Common.Errors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Commands
{
    public class AddSocialMedia
    {
        public class AddSocialMediaCommand : IRequest
        {
            public AddSocialMediaCommand(AddSocialMediaVm vm)
            {
                Vm = vm;
            }

            public AddSocialMediaVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddSocialMediaCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddSocialMediaCommand request, CancellationToken cancellationToken)
            {
                var presenter = await _context.Presenters.FirstOrDefaultAsync(x => x.Id == request.Vm.PresenterId);
                if (presenter == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                var social = new SocialMedia
                {
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.ImgUrl, "presenters"),
                    Link = request.Vm.Link,
                    Provider = request.Vm.Provider,
                    PresenterId=presenter.Id
                };
                _context.SocialMedias.Add(social);
                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    return Unit.Value;
                }
                throw new Exception("Proplem Saving Changes");
            }
        }
    }
}
