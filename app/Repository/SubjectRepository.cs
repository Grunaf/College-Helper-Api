using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationContext _context;
        public SubjectRepository(ApplicationContext context)
        {
            _context = context; 
        }
        public async Task<Subject> CreateAsync(Subject subjectModel)
        {
            await _context.Subjects.AddAsync(subjectModel);
            await _context.SaveChangesAsync();
            return subjectModel;
        }

        public async Task<List<Subject>> GetAllActualByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.Subjects
            .Include(s => s.StudentGroupSubjects)
            .Where(s => s.StudentGroupSubjects.Any(sgs => sgs.StudentGroupId == studentGroupId && !sgs.IsExpired))
            .ToListAsync();
        }
        public async Task<List<Subject>> GetAllByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.Subjects
            .Include(s => s.StudentGroupSubjects)
            .Where(s => s.StudentGroupSubjects.Any(sgs => sgs.StudentGroupId == studentGroupId))
            .ToListAsync();
        }
        public async Task<List<Subject>> GetAllSubjectWhereIsHomeworkByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.Subjects
            .Include(s => s.Homeworks)
            .Where(s => s.Homeworks.Any(sgs => sgs.StudentGroupId == studentGroupId))
            .ToListAsync();
        }
        public async Task<Subject?> GetByNameAsync(string title)
        {
            var subjectModel = await _context.Subjects
                                    .Where(s => s.Title == title)
                                    .FirstOrDefaultAsync();
            return subjectModel;
        }

        public async Task<List<Subject>> GetSubjectsWithHomeworksByStudentGroupIdAsync(List<int> subjectIds, int studentGroupId)
        {
            return await _context.Subjects
                                 .Include(s => s.Homeworks)
                                 .Where(s => subjectIds.Contains(s.Id) && s.Homeworks.Any(h => h.StudentGroupId == studentGroupId))
                                 .ToListAsync();
        }
    }
}
