using Application.Common.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class DeleteEpisode
    {
        public class DeleteEpisodeCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handel : IRequestHandler<DeleteEpisodeCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
            {

                var episode = await _context.Episodes
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if (episode == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }

                _context.Episodes.Remove(episode);
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
