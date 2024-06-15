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
            .Where(s => s.StudentGroupSubjects.Any(sgs => sgs.StudentGroupId == studentGroupId && sgs.IsExpired == false))
            .ToListAsync();
        }
        public async Task<List<Subject>> GetAllByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.Subjects
            .Include(s => s.StudentGroupSubjects)
            .Where(s => s.StudentGroupSubjects.Any(sgs => sgs.StudentGroupId == studentGroupId))
            .ToListAsync();
        }
        public async Task<Subject?> GetByNameAsync(string title)
        {
            var subjectModel = await _context.Subjects
                                    .Where(s => s.Title == title)
                                    .FirstOrDefaultAsync();
            return subjectModel;
        }
    }
}
