using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
    public class AddEpisodeVm
    {
        [Required]
        public int ProgramId { get; set; }
        public int? Number { get; set; }
        [Required]
        public string Title { get; set; }
        public int Duration { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ShowDate { get; set; } = DateTime.UtcNow;
        [Required]
        public IFormFile File { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
    }
}
