using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
    public class ProgramShowTimesVm
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        public string FirstShowTime { get; set; }
    }
}
