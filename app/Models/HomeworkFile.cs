using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class HomeworkFile
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        [MaxLength(255)]
        public string FileId { get; set; }
    }
}
