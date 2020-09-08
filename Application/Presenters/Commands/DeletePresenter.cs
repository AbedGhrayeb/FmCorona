using Application.Common.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Commands
{
    public class DeletePresenter
    {
        public class DeletePresenterCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handel : IRequestHandler<DeletePresenterCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeletePresenterCommand request, CancellationToken cancellationToken)
            {

                var presenter = await _context.Presenters
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if (presenter == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }

                _context.Presenters.Remove(presenter);
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
