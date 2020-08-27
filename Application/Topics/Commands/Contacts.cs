using Application.Interfaces.ExternalAuth;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Commands
{
    public class Contacts
    {
        public class ContactCommand : IRequest
        {
            [Required]
            public string Name { get; set; }
            [DataType(DataType.PhoneNumber)]
            public string Phone { get; set; }
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            public string Message { get; set; }
        }
        public class Handler : IRequestHandler<ContactCommand>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(ContactCommand request, CancellationToken cancellationToken)
            {
                var contact = new Contact
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Message = request.Message
                };
                _context.Contacts.Add(contact);
                if (await _context.SaveChangesAsync() == 0)
                {
                    throw new Exception("Proplem Saving Changes");
                }
                return Unit.Value;
            }
        }
    }
}
