using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class HomeworkRepository(ApplicationContext context) : IHomeworkRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<Homework> CreateAsync(Homework homework)
        {
            await _context.Homeworks.AddAsync(homework);
            await _context.SaveChangesAsync();
            return homework;
        }

        public async Task<Homework> GetByIdAsync(int id)
        {
            var homeworkModel = await _context.Homeworks.Include(h => h.HomeworkFiles).FirstOrDefaultAsync(h => h.Id == id);
            return homeworkModel ?? throw new InvalidOperationException("Домашнее задание не найдено");
        }

        public async Task<List<Homework>> GetBySubjectIdAndGroupIdAsync(int subjectId, int groupId)
        {
            var homeworks = await _context.Homeworks.Where(h => h.SubjectId == subjectId && h.StudentGroupId == groupId).ToListAsync();
            return homeworks ?? throw new InvalidOperationException("Домашнее задание по предмету не найдены");
        }
    }
}
