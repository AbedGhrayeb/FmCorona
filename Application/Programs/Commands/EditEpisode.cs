using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class EditEpisode
    {
        public class EditEpisodeCommand : IRequest
        {
            public EditEpisodeCommand(EditEpisodeVm vm)
            {
                Vm = vm;
            }

            public EditEpisodeVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditEpisodeCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
            {

                var episode = await _context.Episodes.FindAsync(request.Vm.Id);
                if (episode == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                episode.Duration = request.Vm.Duration;
                episode.Guest = request.Vm.Guest;
                episode.GuestName = request.Vm.GuestName ?? episode.GuestName;
                episode.Url = _filesAccessor.ChangeFile(request.Vm.File, episode.Url, "episodes") ?? episode.Url;
                episode.Number = request.Vm.Number;
                episode.Title = request.Vm.Title ?? episode.Title;

                _context.Episodes.Update(episode);
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
