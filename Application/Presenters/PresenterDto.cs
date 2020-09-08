using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Presenters
{
    public class PresenterDto
    {
        public PresenterDto()
        {
            Programs = new List<string>();
        }
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        public string Bio { get; set; }
        [Display(Name = "Image")]
        public string ImgUrl { get; set; }
        public List<string> Programs { get; set; }

    }
}
