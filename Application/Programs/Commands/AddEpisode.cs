using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class AddEpisode
    {
        public class AddEpisodesCommand : IRequest
        {
            public AddEpisodesCommand(AddEpisodeVm vm)
            {
                Vm = vm;
            }

            public AddEpisodeVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddEpisodesCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddEpisodesCommand request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.FindAsync(request.Vm.ProgramId);

                var episode = new Episode
                {
                    Title = request.Vm.Title,
                    Duration = request.Vm.Duration,
                    Number = request.Vm.Number.Value,
                    Program = program,
                    ShowDate = request.Vm.ShowDate,
                    Url = _filesAccessor.UploadFile(request.Vm.File, "episodes"),
                    Guest = request.Vm.Guest,
                    GuestName = request.Vm.GuestName
                };
                _context.Episodes.Add(episode);
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
