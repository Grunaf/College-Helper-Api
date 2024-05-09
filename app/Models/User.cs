using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
    public enum Role
    {
        curator,
        headboy,
        student
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
        public int IdChat { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public int? StudentGroupId { get; set; }
        public StudentGroup? StudentGroup { get; set; }
    }
}
