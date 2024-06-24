using app.Models;

namespace app.Interfaces
{
    public interface IHomeworkFilesRepository
    {
        Task<List<HomeworkFile>> GetAllByStudentGroupIdAsync(int studentGroupId);
    }
}
