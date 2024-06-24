namespace app.Dtos.StudentAbsence
{
    public class GetCountsOfStudentsGroupAbsenceRequestDto
    {
        public DateTime StartOfSemester { get; set; }
        public DateTime EndOfSemester { get; set; }
        public List<CountStudentAbsencesRequestDto> countStudentAbsencesRequestDtos { get; set; }
    }
}
