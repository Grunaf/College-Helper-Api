namespace app.Dtos.Subject
{
    public class GetSheduleDaySubjectRequestDto
    {
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public char? Subgroup { get; set; }
        public byte Spot { get; set; }
    }
}
