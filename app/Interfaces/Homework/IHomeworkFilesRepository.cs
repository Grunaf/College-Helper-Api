using app.Models;

namespace app.Interfaces
{
    public interface IHomeworkFilesRepository
    {
        public Task<List<HomeworkFile>> GetAllByStudentGroupIdAsync(int studentGroupId);
    }
}
