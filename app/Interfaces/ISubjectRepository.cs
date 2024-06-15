using app.Dtos.Subject;
using app.Models;

namespace app.Interfaces
{
    public interface ISubjectRepository
    {
        public Task<Subject> CreateAsync(Subject subjectModel);
        public Task<Subject?> GetByNameAsync(string title);
        public Task<List<Subject>> GetAllActualByStudentGroupIdAsync(int studentGroupId);
        public Task<List<Subject>> GetAllByStudentGroupIdAsync(int studentGroupId);
    }
}
