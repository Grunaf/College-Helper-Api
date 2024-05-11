using app.Dtos.User;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteAsync(long id)
        {
            var userModel = await _context.Users.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(x => x.Id == id);
            if (userModel == null)
            {
                return null;
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(sg => sg.StudentGroup).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            var userModel = await _context.Users.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(u => u.Id == id);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }


        public async Task<List<User>> GetStudentsByIdHeadBoyId(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdChat == id);
            if (user.Role == Role.headboy)
            {
                return await _context.Users.Where(u => u.StudentGroupId == user.StudentGroupId).ToListAsync();
            }
            return null;
        }

        public async Task<User?> UpdateAsync(long id, UpdateUserRequestDto userDto)
        {
            var existingModel = await _context.Users.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(u => u.Id == id);

            if (existingModel == null) return null;

            existingModel.IdChat = userDto.IdChat;
            existingModel.Role = (Role)Enum.Parse(typeof(Role), userDto.Role);
            existingModel.Surname = userDto.Surname;
            existingModel.Name = userDto.Name;
            existingModel.Patronymic = userDto.Patronymic;
            existingModel.StudentGroupId = userDto.StudentGroupId;

            await _context.SaveChangesAsync();

            return existingModel;
        }
    }
}
