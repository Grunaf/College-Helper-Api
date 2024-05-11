using app.Dtos.User;
using app.Models;

namespace app.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetStudentsByIdHeadBoyId(long id);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(long id);
        Task<User> CreateAsync(User userModel);
        Task<User?> DeleteAsync(long id);
        Task<User?> UpdateAsync(long id, UpdateUserRequestDto userDto);
    }
}
