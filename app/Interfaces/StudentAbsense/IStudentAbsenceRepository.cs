using app.Dtos.StudentAbsence;
using app.Models;

namespace app.Interfaces.StudentAbsense
{
    public interface IStudentAbsenceRepository
    {
        public Task<List<StudentAbsence>> GetAllByStudentIdAsync(long studentId);
        public Task<List<StudentAbsence>> GetStatStudentAbsensesByHeadBoyChatIdAsync(long headBoyChatId);
        public Task<List<StudentAbsence>> GetAllByHeadBoyChatIdAsync(long headBoyChatId, DateTime date, byte lessonNumber);
        public Task<StudentAbsence> DeleteByIdAsync(int id);
        public Task<StudentAbsence> CreateAsync(StudentAbsence studentAttendanceModel);
        //public Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto);
    }
}
