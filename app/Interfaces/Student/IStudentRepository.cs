using app.Dtos.Student;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsByStudentGroupIdAsync(int studentGroupId);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int Id);
        Task<Student?> GetByChatIdAsync(long chatId);

        Task<Student> CreateAsync(Student userModel);
        Task<Student> DeleteAsync(int Id);
        Task<Student> UpdateAsync(int Id, Student studentModel);
    }
}
