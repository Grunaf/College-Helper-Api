using System.ComponentModel.DataAnnotations;

namespace app.Dtos.StudentAttendanceRecord
{
    public class StudentAttendanceByStudentIdDto
    {
        public long StudentId { get; set; }
        public DateTime Date { get; set; } 
        public bool MissedFirstLesson { get; set; }
        public bool MissedSecondLesson { get; set; }
        public bool MissedThirdLesson { get; set; }
        public bool MissedFourthLesson { get; set; }
        public bool MissedFifthLesson { get; set; }
        public bool MissedSixthLesson { get; set; }
    }
}
