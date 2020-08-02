using Domain.Entities;
using System.Collections.Generic;

namespace Application.Presenters
{
    public class PresenterDto
    {
        public PresenterDto()
        {
            SocialMediaDtos = new List<SocialMediaDto>();
            Programs = new List<string>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImgUrl { get; set; }
        public List<string> Programs { get; set; }
        public List<SocialMediaDto> SocialMediaDtos { get; set; }
    }
}
