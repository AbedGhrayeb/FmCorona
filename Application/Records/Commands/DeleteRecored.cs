using Application.Common.Errors;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Records.Commands
{
    public class DeleteRecored
    {
        public class DeleteRecoredCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handel : IRequestHandler<DeleteRecoredCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteRecoredCommand request, CancellationToken cancellationToken)
            {
                var recoed = await _context.Records.FindAsync(request.Id);
                if (recoed==null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                _context.Records.Remove(recoed);
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
