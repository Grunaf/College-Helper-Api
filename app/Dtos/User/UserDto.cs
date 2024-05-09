using app.Dtos.StudentGroup;
using app.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public int IdChat { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public int? StudentGroupId { get; set; }
        public StudentGroupDto? StudentGroup { get; set; }
    }
}
