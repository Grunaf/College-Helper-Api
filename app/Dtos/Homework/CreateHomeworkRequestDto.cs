namespace app.Dtos
{
    public class CreateHomeworkRequestDto
    {
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; } = "";
        public List<string> FileIds { get; set; } = [];
    }
}
