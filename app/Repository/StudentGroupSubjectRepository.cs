using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class StudentGroupSubjectRepository(ApplicationContext context) : IStudentGroupSubjectRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<StudentGroupSubject> CreateAsync(StudentGroupSubject studentGroupSubject)
        {
            await _context.StudentGroupSubjects.AddAsync(studentGroupSubject);
            await _context.SaveChangesAsync();
            return studentGroupSubject;
        }

        public async Task<StudentGroupSubject?> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.StudentGroupSubjects.FirstOrDefaultAsync(sgs => sgs.SubjectId == subjectId);
        }

        public async Task<StudentGroupSubject?> UpdateAsync(StudentGroupSubject studentGroupSubject)
        {
            var existingModel = await _context.StudentGroupSubjects.FirstOrDefaultAsync(sgs => 
                                                        sgs.SubjectId == studentGroupSubject.SubjectId &&
                                                        sgs.StudentGroupId == studentGroupSubject.StudentGroupId);

            if (existingModel != null)
            {
                existingModel.StudentGroupId = studentGroupSubject.StudentGroupId;
                existingModel.SubjectId = studentGroupSubject.SubjectId;
                existingModel.IsExpired = studentGroupSubject.IsExpired;
            }

            await _context.SaveChangesAsync();
            return studentGroupSubject;
        }
    }
}
