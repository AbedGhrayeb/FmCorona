using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public string RecordUrl { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
