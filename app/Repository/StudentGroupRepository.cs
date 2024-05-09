using app.Dtos.StudentGroup;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private readonly ApplicationContext _context;
        public StudentGroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<StudentGroup> CreateAsync(StudentGroup studenModel)
        {
            await _context.StudentGroups.AddAsync(studenModel);
            await _context.SaveChangesAsync();

            return studenModel;
        }

        public async Task<StudentGroup?> DeleteAsync(int id)
        {
            var studentGroupModel = await _context.StudentGroups.FirstOrDefaultAsync(sg => sg.Id == id);

            if (studentGroupModel == null) return null;

            _context.StudentGroups.Remove(studentGroupModel);
            await _context.SaveChangesAsync();

            return studentGroupModel;
        }

        public async Task<List<StudentGroup>> GetAllAsync()
        {
            return await _context.StudentGroups.ToListAsync();
        }

        public async Task<StudentGroup?> GetByIdAsync(int id)
        {
            var studentGroupModel = await _context.StudentGroups.FindAsync(id);
            if (studentGroupModel == null) return null;
            return studentGroupModel;
        }

        public async Task<StudentGroup> UpdateAsync(int id, UpdateStudentGroupRequestDto studentGroupDto)
        {
            var existingModel = await _context.StudentGroups.FirstOrDefaultAsync(sg => sg.Id == id);
            if (existingModel == null) return null;

            existingModel.Field = (Field)Enum.Parse(typeof(Field), studentGroupDto.Field);
            existingModel.Number = studentGroupDto.Number;
            existingModel.HeadBoyId = studentGroupDto.HeadBoyId;
            existingModel.CuratorId = studentGroupDto.CuratorId;

            await _context.SaveChangesAsync();

            return existingModel;
        }
    }
}
