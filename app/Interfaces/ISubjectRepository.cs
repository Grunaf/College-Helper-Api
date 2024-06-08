using app.Dtos.Subject;
using app.Models;

namespace app.Interfaces
{
    public interface ISubjectRepository
    {
        public Task<Subject> CreateAsync(Subject subjectModel);
        public Task<Subject?> GetByNameAsync(string title);
    }
}
