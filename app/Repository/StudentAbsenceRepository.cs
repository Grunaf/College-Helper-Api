using app.Dtos.StudentAbsence;
using app.Interfaces.StudentAbsense;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentAbsenceRepository : IStudentAbsenceRepository
    {
        private readonly ApplicationContext _context;
        public StudentAbsenceRepository (ApplicationContext context)
        {
            _context = context;
        }

        public async Task<StudentAbsence> CreateAsync(StudentAbsence studentAttendanceModel)
        {
            await _context.StudentAbsence.AddAsync(studentAttendanceModel);
            await _context.SaveChangesAsync();
            return studentAttendanceModel;
        }

 
        public async Task<StudentAbsence> DeleteByIdAsync(int id)
        {
            var absence = await _context.StudentAbsence.FindAsync(id);
            if (absence == null)
            {
                return null;
            }
            _context.StudentAbsence.Remove(absence);
            await _context.SaveChangesAsync();
            return absence;
        }

        public async Task<List<StudentAbsence>> GetAllByStudentGroupIdAsync(int studentGroupId, DateTime date, byte lessonNumber)
        {
            return await _context.StudentAbsence.Include(s => s.Student).
                Where(sa => sa.Student.StudentGroupId == studentGroupId).
                Where(sa => sa.Date == date).Where(sa => sa.LessonNumber == lessonNumber).
                ToListAsync();
        }

        public async Task<List<StudentAbsence>> GetAllByStudentIdAsync(long studentId)
        {
            return await _context.StudentAbsence.Where(sa => sa.StudentId == studentId).ToListAsync();
        }

        public async Task<List<StudentAbsence>> GetStatStudentAbsencesByStudentGroupIdAsync(int studentGroupId)
        {
            var startEducationDate = DateTime.Now.Month >= 9 ? new DateTime(DateTime.Now.Year, 9, 1) : new DateTime(DateTime.Now.Year - 1, 9, 1);

            return await _context.StudentAbsence.Include(s => s.Student).
                Where(sa => sa.Student.StudentGroupId == studentGroupId).
                Where(sa => sa.Date >= startEducationDate).ToListAsync();
        }
        /*
       public async Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto)
       {
           var existingModel = await _context.StudentAttendances.FirstOrDefaultAsync(sa => sa.Id == studentAttendanceDto.Id);
       }*/
    }
}
