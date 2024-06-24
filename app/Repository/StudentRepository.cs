using app.Dtos.Student;
using app.Exceptions;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentRepository(ApplicationContext context) : IStudentRepository
    {
        private readonly ApplicationContext _context = context;

        public async Task<Student> CreateAsync(Student userModel)
        {
            await _context.Students.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<Student> DeleteAsync(int Id)
        {
            var userModel = await _context.Students.FindAsync(Id);
            if (userModel == null)
            {
                throw new DataNotFoundException("Студент не найден");
            }

            _context.Students.Remove(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.Include(sg => sg.StudentGroup).ToListAsync();
        }

        public async Task<Student?> GetByChatIdAsync(long chatId)
        {
            var userModel = await _context.Students.FirstOrDefaultAsync(s => s.ChatId == chatId);
            return userModel;
        }

        public async Task<Student> GetByIdAsync(int Id)
        {
            var userModel = await _context.Students.FindAsync(Id);
            return userModel ?? throw new DataNotFoundException("Студент не найден");
        }

        public async Task<List<Student>> GetStudentsByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.Students.Where(u => u.StudentGroupId == studentGroupId).ToListAsync();
        }

        public async Task<Student> UpdateAsync(int Id, Student studentModel)
        {
            var existingModel = await _context.Students.FindAsync(Id);

            if (existingModel == null) throw new DataNotFoundException("Студент не найден");

            existingModel.ChatId = studentModel.ChatId;
            existingModel.IsHeadBoy = studentModel.IsHeadBoy;
            existingModel.Surname = studentModel.Surname;
            existingModel.Name = studentModel.Name;
            existingModel.Patronymic = studentModel.Patronymic;

            await _context.SaveChangesAsync();
            return existingModel;
        }
    }
}
