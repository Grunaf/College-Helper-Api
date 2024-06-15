using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public int StudentGroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedTime { get; set; } = DateTime.Today;
        public List<HomeworkFile> HomeworkFiles { get; set; } = [];
    }
}
