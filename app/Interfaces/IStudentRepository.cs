using app.Dtos.Student;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsByIdHeadBoyId(long id);
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(long id);
        Task<Student> CreateAsync(Student userModel);
        Task<Student?> DeleteAsync(long id);
        Task<Student?> UpdateAsync(long id, UpdateStudentRequestDto userDto);
    }
}
