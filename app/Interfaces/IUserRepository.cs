using app.Dtos.User;
using app.Models;

namespace app.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User userModel);
        Task<User?> DeleteAsync(int id);
        Task<User?> UpdateAsync(int id, UpdateUserRequestDto userDto);
    }
}
