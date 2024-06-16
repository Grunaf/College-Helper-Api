using app.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Dtos.Student
{
    public class CreateStudentRequestDto
    {
        public bool IsHeadBoy { get; set; }
        public int IdChat { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public int StudentGroupId { get; set; }
    }
}
