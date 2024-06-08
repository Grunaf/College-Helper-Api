namespace app.Models
{
    public class SheduleDay
    {
        public int Id { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public byte CountWeek { get; set; }
        public byte CountDay { get; set; }
        public List<SheduleDaySubject> SheduleDaySubjects { get; set; } = [];
    }
}
