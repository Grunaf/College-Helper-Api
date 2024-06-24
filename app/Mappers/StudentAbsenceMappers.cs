using app.Dtos.StudentAbsence;
using app.Models;
using System.Runtime.CompilerServices;

namespace app.Mappers
{
    public static class StudentAbsenceMappers
    {
/*        public static StudentAttendanceByStudentIdDto ToStudentAttendanceByStudentIdDto(this StudentAbsence studentAttendanceModel)
        {
            return new StudentAttendanceByStudentIdDto
            {
                StudentId = studentAttendanceModel.StudentId,
                Date = studentAttendanceModel.Date
            };
        }*/
        public static StudentAbsence ToStudentAbsence (this CreateOrDeleteStudentAbsenceRequestDto absenceDto)
        {
            return new StudentAbsence
            {
                StudentId = absenceDto.StudentId,
                LessonNumber = absenceDto.LessonNumber,
                Date = DateTime.Today
            };
        }
/*        public static StatStudentAbsenseRequestDto ToStatStudentAbsenseDtoFromStudentAbsense(this StudentAbsence studentAbsenceModel)
        {
            return new StatStudentAbsenseRequestDto
            {
                Name = studentAbsenceModel.Student.Name,
                Surname = studentAbsenceModel.Student.Surname,
                Patronymic = studentAbsenceModel.Student.Patronymic
            };
        }*/
    }
}
