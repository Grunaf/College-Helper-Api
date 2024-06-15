namespace app.Dtos.Subject
{
    public class GetSheduleDaySubjectRequestDto
    {
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public string? SubgroupSequence { get; set; }
        public byte Spot { get; set; }
    }
}
