using app.Dtos.Student;
using app.Dtos.StudentAbsence;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetHeadBoyOrThrowExceptionAsync(long headBoyChatId);
        //Task<List<StudentAbsenceDto>> GetStatusStudentFromListAttendanceAsync(long headBoyChatId, DateTime date, byte lessonNumber);
        Task<List<Student>> GetStudentsByStudentGroupIdAsync(int studentGroupId);
        Task<StudentRoleInfoDto> GetInfoByChatIdAsync(long chatId);

        Task<Student> GetStudentOrThrowExceptionAsync(long studentChatId);
        Task CheckAndThrowIfStudentExistsAsync(long studentChatId);
        Task<Student> GetByIdAsync(int studentId);
        Task<Student?> GetByChatIdAsync(long chatId);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(int studentId, Student student);
        Task<Student> DeleteAsync(int studentId);
    }
}
