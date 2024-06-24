namespace app.Dtos.Student
{
    public class StudentDto
    {
        public bool IsHeadBoy { get; set; } = false;
        public long ChatId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
    }
}
