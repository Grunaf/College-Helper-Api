namespace app.Dtos.StudentAbsence
{
    public class CreateStudentAbsenceRequestDto
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public byte LessonNumber { get; set; }
    }
}
