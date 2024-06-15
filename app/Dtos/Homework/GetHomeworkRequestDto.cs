namespace app.Dtos
{
    public class GetHomeworkRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
