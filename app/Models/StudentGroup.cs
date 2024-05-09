using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Models
{
    public enum Field
    {
        БАС,
        ИСП
    }


    public class StudentGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Field Field { get; set; }
        [Required]
        public int Number { get; set; }
        public int? CuratorId { get; set; }
        public User? Curator { get; set; }
        public int? HeadBoyId { get; set; }
        public User? HeadBoy { get; set; }
        public List<User?> Students { get; set; } = new List<User?>();
    }
}
