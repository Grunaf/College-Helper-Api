using app.Dtos.Student;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupService
    {
        Task<StudentGroup> GetGroupByHeadBoyChatIdAsync(long headBoyChatId);
        Task<StudentGroup> GetGroupByStudentChatIdAsync(long studentChatId);
        Task CheckIsStudentInSomeGroupOrThrowExceptionAsync(int studentId, int studentGroupId);
        Task CheckIsStudentsInGroupOrThrowExceptionAsync(int studentGroupId);
        Task<List<StudentDto>> AddStudentsToGroupAsync(long headBoyChatId, List<StudentDto> studentsDto);
        Task<StudentDto> UpdateStudentInGroupAsync(long headBoyChatId, int studentId, StudentDto studentDto);
        Task<Student> DeleteStudentInGroupAsync(long headBoyChatId, long studentChatId);
        Task<List<GetStudentRequestDto>> GetAllStudentsExceptHeadBoyInGroupFromHeadBoy(long headBoyChatId);
    }
}
