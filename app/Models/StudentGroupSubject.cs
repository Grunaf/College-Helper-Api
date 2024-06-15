namespace app.Models
{
    public class StudentGroupSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int StudentGroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public bool IsExpired { get; set; } = false;
    }
}
