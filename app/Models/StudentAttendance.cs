using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; } 
        public DateTime Date { get; set; } = DateTime.Today;
        public byte NumberLesson { get; set; }
        public Student Student { get; set; }
    }
}
