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
        public static StudentAbsenceDto ToStudentAttendanceByIdDto(this StudentAbsence studentAttendanceModel)
        {
            return new StudentAbsenceDto
            {
                StudentId = studentAttendanceModel.StudentId
            };
        }
/*        public static StudentAbsence ToStudentAttendanceFromCreateDto(this CreateStudentAttendanceRequestDto studentAttendanceDto)
        {
            return new StudentAbsence
            {
                StudentId = studentAttendanceDto.StudentId
            };
        }*/
        public static StudentAbsence ToStudentAbsenceFromCreateOrDeleteDto(this CreateOrDeleteStudentAbsenceRequestDto studentAbsenceDto)
        {
            return new StudentAbsence
            {
                StudentId = studentAbsenceDto.StudentId,
                LessonNumber = studentAbsenceDto.LessonNumber,
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
