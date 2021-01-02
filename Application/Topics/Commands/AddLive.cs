using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Commands
{
    public class AddLive
    {
        public class AddLiveCommand : IRequest
        {
            [Required]
            [DataType(DataType.Url)]
            public string Url { get; set; }
        }
        public class Handler : IRequestHandler<AddLiveCommand>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(AddLiveCommand request, CancellationToken cancellationToken)
            {
                var lives = _context.Lives;
                if (lives.Count() > 0) 
                {
                    _context.Lives.RemoveRange(lives);
                }
                var live = new Live { CreatAt = DateTime.UtcNow, Url = request.Url };
                _context.Lives.Add(live);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Proplem Saving Changes");
            }
        }
    }
}
