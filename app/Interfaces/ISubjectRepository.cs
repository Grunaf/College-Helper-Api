using app.Dtos.Subject;
using app.Models;

namespace app.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> CreateAsync(Subject subjectModel);
        Task<Subject?> GetByNameAsync(string title);
        Task<List<Subject>> GetAllActualByStudentGroupIdAsync(int studentGroupId);
        Task<List<Subject>> GetAllByStudentGroupIdAsync(int studentGroupId);
        Task<List<Subject>> GetAllSubjectWhereIsHomeworkByStudentGroupIdAsync(int studentGroupId);
        Task<List<Subject>> GetSubjectsWithHomeworksByStudentGroupIdAsync(List<int> subjectIds, int studentGroupId);
    }
}
