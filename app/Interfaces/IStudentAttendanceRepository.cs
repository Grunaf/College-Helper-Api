using app.Dtos.StudentAttendance;
using app.Dtos.StudentAttendanceRecord;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentAttendanceRepository
    {
        public Task<List<StudentAttendance>> GetAllByStudentIdAsync(long studentId);
        public Task<StudentAttendance> CreateAsync(StudentAttendance studentAttendanceModel);
        //public Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto);
    }
}
