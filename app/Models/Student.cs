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
        public bool IsHeadBoy { get; set; }
        public long ChatId { get; set; }
        public string? Surname { get; set; } = "";
        public string? Name { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public int StudentGroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public List<StudentAbsence> StudentAbsence { get; set; }
    }
}
