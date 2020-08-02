using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Identity
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string ImgUrl { get; set; }
    }
}
