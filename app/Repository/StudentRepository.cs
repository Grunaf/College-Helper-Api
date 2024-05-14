using app.Dtos.Student;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateAsync(Student userModel)
        {
            await _context.Students.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<Student?> DeleteAsync(long id)
        {
            var userModel = await _context.Students.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(x => x.Id == id);
            if (userModel == null)
            {
                return null;
            }

            _context.Students.Remove(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.Include(sg => sg.StudentGroup).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(long id)
        {
            var userModel = await _context.Students.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(u => u.Id == id);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }


        public async Task<List<Student>> GetStudentsByIdHeadBoyId(long id)
        {
            var user = await _context.Students.FirstOrDefaultAsync(u => u.IdChat == id);
            if (user.IsHeadBoy)
            {
                return await _context.Students.Where(u => u.StudentGroupId == user.StudentGroupId).ToListAsync();
            }
            return null;
        }

        public async Task<Student?> UpdateAsync(long id, UpdateStudentRequestDto studentDto)
        {
            var existingModel = await _context.Students.Include(sg => sg.StudentGroup).FirstOrDefaultAsync(u => u.Id == id);

            if (existingModel == null) return null;

            existingModel.IdChat = studentDto.IdChat;
            existingModel.IsHeadBoy = studentDto.IsHeadboy;
            existingModel.Surname = studentDto.Surname;
            existingModel.Name = studentDto.Name;
            existingModel.Patronymic = studentDto.Patronymic;
            existingModel.StudentGroupId = studentDto.StudentGroupId;

            await _context.SaveChangesAsync();

            return existingModel;
        }
    }
}
