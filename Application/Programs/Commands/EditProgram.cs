using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class EditProgram
    {
        public class EditProgramCommand : IRequest
        {
            public EditProgramCommand(EditProgramVm vm)
            {
                Vm = vm;
            }

            public EditProgramVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditProgramCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditProgramCommand request, CancellationToken cancellationToken)
            {
                var presenter = await _context.Presenters.FindAsync(request.Vm.PresenterId);

                var program = await _context.Programs.FindAsync(request.Vm.Id);
                if (program == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                program.DefaultDuration = request.Vm.DefaultDuration;
                program.Description = request.Vm.Description ?? program.Description;
                program.Name = request.Vm.Name ?? program.Name;
                program.ImgUrl = _filesAccessor.ChangeFile(request.Vm.Image, program.ImgUrl, "programs") ?? program.ImgUrl;
                program.Presenter = presenter;

                _context.Programs.Update(program);
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
