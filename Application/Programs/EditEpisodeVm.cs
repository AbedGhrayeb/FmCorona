using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Programs
{
    public class EditEpisodeVm
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int? Number { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
    }
}
