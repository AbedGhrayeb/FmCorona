using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            ExternalLogins = new HashSet<ExternalLogin>();
            FavoriteArtists = new HashSet<FavoriteArtist>();
            Records = new HashSet<Record>();
        }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImgUrl { get; set; }
        public virtual ICollection<ExternalLogin> ExternalLogins { get; set; }
        public virtual ICollection<FavoriteArtist> FavoriteArtists { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
