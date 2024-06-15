namespace app.Dtos
{
    public class GetFullHomeworkRequestDto
    {
        public string Title { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedTime { get; set; }
        public List<string> FileIds { get; set; } = [];
    }
}
