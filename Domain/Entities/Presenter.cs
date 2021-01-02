using System.Collections.Generic;

namespace Domain.Entities
{
    public class Presenter
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImgUrl { get; set; }
        public virtual ICollection<Program> Programs { get; set; }

    }
}
