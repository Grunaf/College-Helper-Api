using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class HomeworkFilesRepository(ApplicationContext context) : IHomeworkFilesRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<List<HomeworkFile>> GetAllByStudentGroupIdAsync(int studentGroupId)
        {
            return await _context.HomeworkFiles.Include(hf => hf.Homework)
                        .Where(hf => hf.Homework.StudentGroupId == studentGroupId)
                        .ToListAsync();
        }
    }
}
