using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Commands
{
    public class DeleteArtical
    {
        public class DeleteArticalCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handel : IRequestHandler<DeleteArticalCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteArticalCommand request, CancellationToken cancellationToken)
            {

                var artical = await _context.Articals.FindAsync(request.Id);
                if (artical == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }

                _context.Articals.Remove(artical);
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
