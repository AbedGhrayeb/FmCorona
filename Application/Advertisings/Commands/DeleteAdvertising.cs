using Application.Common.Errors;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Advertisings.Commands
{
    public class DeleteAdvertising
    {
        public class DeleteAdvertisingCommand : IRequest
        {

            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<DeleteAdvertisingCommand>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteAdvertisingCommand request, CancellationToken cancellationToken)
            {
                var advertising = await _context.Advertisings.FindAsync(request.Id);
                if (advertising == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                _context.Advertisings.Remove(advertising);
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
