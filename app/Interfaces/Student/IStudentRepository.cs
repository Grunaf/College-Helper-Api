using app.Dtos.Student;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsByHeadBoyChatIdAsync(long chatId);
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByChatIdAsync(long chatId);

        Task<Student> CreateAsync(Student userModel);
        Task<Student?> DeleteAsync(long id);
        Task<Student?> UpdateAsync(long id, UpdateStudentRequestDto userDto);
    }
}
