using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        [Required]
        public long StudentId { get; set; } 
        public DateTime Date { get; set; } = DateTime.Today;
        public byte NumberLesson { get; set; }
        public User Student { get; set; }
    }
}
