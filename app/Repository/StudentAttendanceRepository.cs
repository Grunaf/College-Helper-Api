using app.Dtos.StudentAttendance;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentAttendanceRepository : IStudentAttendanceRepository
    {
        private readonly ApplicationContext _context;
        public StudentAttendanceRepository (ApplicationContext context)
        {
            _context = context;
        }

        public async Task<StudentAttendance> CreateAsync(StudentAttendance studentAttendanceModel)
        {
            await _context.StudentAttendances.AddAsync(studentAttendanceModel);
            await _context.SaveChangesAsync();
            return studentAttendanceModel;
        }

        public async Task<List<StudentAttendance>> GetAllByStudentIdAsync(long studentId)
        {
            return await _context.StudentAttendances.Where(sa => sa.StudentId == studentId).ToListAsync();
        }
/*
        public async Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto)
        {
            var existingModel = await _context.StudentAttendances.FirstOrDefaultAsync(sa => sa.Id == studentAttendanceDto.Id);
        }*/
    }
}
