namespace app.Dtos.StudentAbsence
{
    public class CreateOrDeleteStudentAbsenceRequestDto
    {
        public int StudentId { get; set; }
        public byte LessonNumber { get; set; }
    }
}
