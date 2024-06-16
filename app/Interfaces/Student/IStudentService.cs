using app.Dtos.StudentAbsence;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentService
    {
        public Task<List<StudentAbsenceDto>> GetStatusStudentFromListAttendanceAsync(long headBoyChatId, DateTime date, byte lessonNumber);
        public Task<bool> IsStudentHeadBoyByChatIdAsync(long chatId);
    }
}
