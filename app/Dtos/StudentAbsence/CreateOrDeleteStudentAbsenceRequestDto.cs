namespace app.Dtos.StudentAbsence
{
    public class CreateOrDeleteStudentAbsenceRequestDto
    {
        public int? AbsenceId { get; set; }
        public int StudentId { get; set; }
        public byte LessonNumber { get; set; }
    }
}
