namespace app.Dtos
{
    public class UpdateHomeworkRequestDto
    {
        public string Title { get; set; }
        public string Comment { get; set; } = "";
        public List<string> FileIds { get; set; } = [];
    }
}
