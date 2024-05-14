using app.Dtos.Student;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Dtos.StudentGroup
{
    public class StudentGroupDto
    {
        public int Id { get; set; }
        public string? Field { get; set; }
        public int Number { get; set; }
        public int? CuratorId { get; set; }
        //public UserDto? Curator { get; set; }
        public int? HeadBoyId { get; set; }
        //public UserDto? HeadBoy { get; set; }
    }
}
