using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class AddProgram
    {
        public class AddProgramCommand : IRequest
        {
            public AddProgramCommand(AddProgramVm vm)
            {
                Vm = vm;
            }

            public AddProgramVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddProgramCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddProgramCommand request, CancellationToken cancellationToken)
            {
                var presenter = await _context.Presenters.FindAsync(request.Vm.PresinterId);

                var program = new Program
                {
                    Name = request.Vm.Name,
                    Description = request.Vm.Description,
                    DefaultDuration = request.Vm.DefaultDuration,
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.Image,"programs"),
                    Presenter = presenter
                };
                _context.Programs.Add(program);
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
