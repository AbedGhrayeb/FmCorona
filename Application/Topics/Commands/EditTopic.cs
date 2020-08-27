using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Errors;
using MediatR;
using Persistence;

namespace Application.Topics.Commands
{
    public class EditTopic
    {
        public class EditTopicCommand : IRequest
        {
            public EditTopicVm Vm { get; }
            public EditTopicCommand(EditTopicVm vm)
            {
                this.Vm = vm;

            }
        }
        public class Handler : IRequestHandler<EditTopicCommand>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Unit> Handle(EditTopicCommand request, CancellationToken cancellationToken)
            {
                var topic = await _context.Topics.FindAsync(request.Vm.Id);
                if (topic==null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                topic.Title=request.Vm.Title??topic.Title;
                topic.Body=request.Vm.Body??topic.Body;

                _context.Topics.Update(topic);
                    if (await _context.SaveChangesAsync() == 0)
                {
                    throw new Exception("Proplem Saving Changes");
                }
                return Unit.Value;
            }
        }
    }
}