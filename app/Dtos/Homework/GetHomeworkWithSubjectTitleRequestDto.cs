namespace app.Dtos
{
    public class GetHomeworkWithSubjectTitleRequestDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string SubjectTitle { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
