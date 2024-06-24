using app.Dtos.StudentGroup;
using app.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Dtos.Student
{
    public class GetStudentRequestDto
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
/*        public int? StudentGroupId { get; set; }
        public StudentGroupDto? StudentGroup { get; set; }*/
    }
}
