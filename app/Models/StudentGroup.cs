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
        public int Id { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Field Field { get; set; }
        [Required]
        public int Number { get; set; }
        public int? CuratorId { get; set; }
        public Professor? Curator { get; set; }
        public int? HeadBoyId { get; set; }
        public Student? HeadBoy { get; set; }
        public List<Student?> Students { get; set; } = new List<Student?>();
    }
}
