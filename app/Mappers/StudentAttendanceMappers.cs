using app.Dtos.StudentAttendance;
using app.Dtos.StudentAttendanceRecord;
using app.Models;
using System.Runtime.CompilerServices;

namespace app.Mappers
{
    public static class StudentAttendanceMappers
    {
        public static StudentAttendanceByStudentIdDto ToStudentAttendanceByStudentIdDto(this StudentAttendance studentAttendanceModel)
        {
            return new StudentAttendanceByStudentIdDto
            {
                StudentId = studentAttendanceModel.StudentId,
                Date = studentAttendanceModel.Date
            };
        }
        public static StudentAttendanceByIdDto ToStudentAttendanceByIdDto(this StudentAttendance studentAttendanceModel)
        {
            return new StudentAttendanceByIdDto
            {
                Id = studentAttendanceModel.Id,
                Date = studentAttendanceModel.Date
            };
        }
        public static StudentAttendance ToStudentAttendanceFromCreateDto(this StudentAttendanceCreateRequestDto studentAttendanceDto)
        {
            return new StudentAttendance
            {
                StudentId = studentAttendanceDto.StudentId
            };
        }
        public static StudentAttendance ToStudentAttendanceFromUpdateDto(this StudentAttendanceUpdateRequestDto studentAttendanceDto)
        {
            return new StudentAttendance
            {
                StudentId = studentAttendanceDto.Id
            };
        }
    }
}
