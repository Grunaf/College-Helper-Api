namespace app.Dtos.StudentAttendance
{
    public class StudentAttendanceCreateRequestDto
    {
        public long StudentId { get; set; }
        public bool MissedFirstLesson { get; set; }
        public bool MissedSecondLesson { get; set; }
        public bool MissedThirdLesson { get; set; }
        public bool MissedFourthLesson { get; set; }
        public bool MissedFifthLesson { get; set; }
        public bool MissedSixthLesson { get; set; }
    }
}
