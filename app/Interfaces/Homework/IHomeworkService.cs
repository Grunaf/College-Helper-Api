using app.Dtos;
using app.Models;

namespace app.Interfaces
{
    public interface IHomeworkService
    {
        Task<CreateHomeworkRequestDto> AddByHeadBoyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto);
        Task<GetFullHomeworkRequestDto> GetByIdByStudentChatIdAsync(long studentChatId, int homeworkId);
        Task<List<GetHomeworkRequestDto>> GetBySubjectIdAndStudentChatIdAsync(long studentChatId, int subjectId);
        Task<CreateHomeworkRequestDto> UpdateByHeadBoyChatIdAsync(long headBoyChatId, int homeworkId, CreateHomeworkRequestDto createHomeworkRequestDto);
        Task<List<Homework>> GetHomeworksForSubjects(List<int> subjectIds, int studentGroupId);
    }
}
