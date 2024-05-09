namespace app.Models
{
    public enum TypeSubject
    {
        Default,
        Lecture,
        Lab
    }
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeSubject TypeSubject { get; set; }
        public bool isExpired { get; set; } = false;
    }
}
