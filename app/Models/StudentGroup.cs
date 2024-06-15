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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Field Field { get; set; }
        public int Number { get; set; }
        public int? CuratorId { get; set; }
        public Professor Curator { get; set; }
        public Student HeadBoy { get; set; }
        public List<StudentGroupSubject> StudentGroupSubjects { get; set; } = [];
        public List<Homework> Homeworks { get; set; } = [];
    }
}
