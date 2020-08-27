using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Queries
{
    public class ContactsList
    {
        public class ContactsListQuery : IRequest<List<Contact>> { }
        public class Hndler : IRequestHandler<ContactsListQuery, List<Contact>>
        {
            private readonly DataContext _context;

            public Hndler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Contact>> Handle(ContactsListQuery request, CancellationToken cancellationToken)
            {
                var contacts = await _context.Contacts.ToListAsync();
                contacts.OrderByDescending(x => x.Id);
                return contacts;
            }
        }
    }
}
