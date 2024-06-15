namespace app.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Homework> Homeworks { get; set; } = [];
        public List<SheduleDaySubject> SheduleDaySubjects { get; set; } = [];
        public List<StudentGroupSubject> StudentGroupSubjects { get; set; } = [];
    }
}
