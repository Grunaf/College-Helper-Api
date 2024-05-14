using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
/*    public enum Role
    {
        curator,
        headboy,
        student
    }
*/
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public bool IsHeadBoy { get; set; }
        [Required]
        public long IdChat { get; set; }
        public string? Surname { get; set; } = "";
        public string? Name { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public int? StudentGroupId { get; set; }
        public StudentGroup? StudentGroup { get; set; }
        public List<StudentAttendance> studentAttendances { get; set; }
    }
}
