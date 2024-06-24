using app.Dtos.Subject;

namespace app.Dtos.SheduleDay
{
    public class GetSheduleDayRequestDto
    {
        public string dayOfWeek { get; set; }
        public byte numOfWeek { get; set; }
        public List<GetSheduleDaySubjectRequestDto?> SubjectDtos { get; set; }
        public List<GetHomeworkWithSubjectTitleRequestDto> HomeworksDto { get; set; }
    }
}
