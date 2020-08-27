using Application.Common.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class DeleteProgram
    {
        public class DeleteProgramCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handel : IRequestHandler<DeleteProgramCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
            {

                var program = await _context.Programs
                    .Include(x => x.Episodes)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if (program == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }

                _context.Programs.Remove(program);
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
