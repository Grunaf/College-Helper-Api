using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public bool IsCurator { get; set; } = false;
        public long IdChat { get; set; }
        public string? Surname { get; set; } = "";
        public string? Name { get; set; } = "";
        public string? Patronymic { get; set; } = "";
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
