using app.Dtos;
using app.Models;

namespace app.Interfaces
{
    public interface IHomeworkRepository
    {
        public Task<Homework> CreateAsync(Homework homework);
        public Task<Homework> GetByIdAsync(int id);
        public Task<List<Homework>> GetBySubjectIdAndGroupIdAsync(int subjectId, int groupId);
    }
}
