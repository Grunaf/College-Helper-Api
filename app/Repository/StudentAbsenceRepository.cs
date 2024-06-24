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
            var absence = await _context.StudentAbsence.FindAsync(id) ?? throw new InvalidOperationException("Запись об отсутствии не найдена");

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
        public async Task<StudentAbsence> GetByStudentIdAndLessonNumberAsync(long studentId, int lessonNumer, DateTime date)
        {
            return await _context.StudentAbsence.Where(sa => sa.StudentId == studentId && sa.LessonNumber == lessonNumer && sa.Date == date).FirstOrDefaultAsync();
        }

        public async Task<List<StudentAbsence>> GetAbsencesCountForPeriodByStudentGroupId(int studentGroupId, DateTime fromDate, DateTime toDate)
        {
            return await _context.StudentAbsence.Include(s => s.Student).
                Where(sa => sa.Student.StudentGroupId == studentGroupId).
                Where(sa => sa.Date >= fromDate && sa.Date <= toDate).ToListAsync();
        }
        /*
       public async Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto)
       {
           var existingModel = await _context.StudentAttendances.FirstOrDefaultAsync(sa => sa.Id == studentAttendanceDto.Id);
       }*/
    }
}
