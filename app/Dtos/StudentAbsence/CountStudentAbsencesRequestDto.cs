namespace app.Dtos.StudentAbsence
{
    public class CountStudentAbsencesRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int CountAbsence { get; set; }
    }
}
