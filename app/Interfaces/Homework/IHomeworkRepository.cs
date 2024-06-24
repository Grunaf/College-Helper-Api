using app.Dtos;
using app.Models;

namespace app.Interfaces
{
    public interface IHomeworkRepository
    {
        Task<Homework> CreateAsync(Homework homework);
        Task<Homework> GetByIdAsync(int id);
        Task<List<Homework>> GetBySubjectIdAndStudentChatIdAsync(int subjectId, int groupId);
        Task<List<Homework>> GetBySubjectIdAndGroupIdIncludeSubjectAsync(int subjectId, int groupId);
        Task<List<Homework>> GetHomeworksForSubjectsByStudentGroupIdAsync(List<int> subjectIds, int studentGroupId);
        Task<Homework> DeleteAsync(int homeworkId);
    }
}
