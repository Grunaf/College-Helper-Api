using app.Dtos.Subject;
using app.Models;

namespace app.Interfaces
{
    public interface ISubjectService
    {
        Task<List<GetSubjectRequestDto>> GetSubjectsWithHomeworkByStudentChatIdAsync(long studentChatId);
        Task<List<GetSubjectRequestDto>> GetActualSubjectsByStudentChatIdAsync(long studentChatId);
    }
}
