//using Application.Common.Errors;
//using Domain.Entities;
//using MediatR;
//using Persistence;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Application.Programs.Queries
//{
//    public class ShowTimeDetails
//    {
//        public class ShowTimeDetailsQuery : IRequest<ShowTime>
//        {
//            public int Id { get; set; }
//        }
//        public class Handler : IRequestHandler<ShowTimeDetailsQuery, ShowTime>
//        {
//            private readonly DataContext _context;

//            public Handler(DataContext context)
//            {
//                _context = context;
//            }
//            public async Task<ShowTime> Handle(ShowTimeDetailsQuery request, CancellationToken cancellationToken)
//            {
//                var showTime = await _context.ShowTimes.FindAsync(request.Id);
//                if (showTime == null)
//                {
//                    throw new RestException(HttpStatusCode.NotFound);
//                }

//                return showTime;
//            }
//        }
//    }
//}
