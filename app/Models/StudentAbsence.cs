using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class StudentAbsence
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public byte LessonNumber { get; set; }
    }
}
