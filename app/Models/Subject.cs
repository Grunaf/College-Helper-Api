namespace app.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SheduleDaySubject> SheduleDaySubjects { get; set; } = new List<SheduleDaySubject>();
        public bool IsExpired { get; set; } = false;
    }
}
