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
        public async Task<Subject?> GetByNameAsync(string title)
        {
            var subjectModel = await _context.Subjects
                                    .Where(s => s.Title == title)
                                    .FirstOrDefaultAsync();
            return subjectModel;
        }
    }
}
