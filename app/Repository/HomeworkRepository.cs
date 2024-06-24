using app.Dtos;
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

        public async Task<List<Homework>> GetBySubjectIdAndStudentChatIdAsync(int subjectId, int groupId)
        {
            var homeworks = await _context.Homeworks.Where(h => h.SubjectId == subjectId && h.StudentGroupId == groupId).ToListAsync();
            return homeworks ?? throw new InvalidOperationException("Домашнее задание по предмету не найдены");
        }
        public async Task<List<Homework>> GetBySubjectIdAndGroupIdIncludeSubjectAsync(int subjectId, int groupId)
        {
            var homeworks = await _context.Homeworks.Where(h => h.SubjectId == subjectId && h.StudentGroupId == groupId).ToListAsync();
            return homeworks ?? throw new InvalidOperationException("Домашнее задание по предмету не найдены");
        }

        public async Task<List<Homework>> GetHomeworksForSubjectsByStudentGroupIdAsync(List<int> subjectIds, int studentGroupId)
        {
            return await _context.Homeworks.Include(h => h.Subject)
                                 .Where(h => subjectIds.Contains(h.SubjectId) && h.StudentGroupId == studentGroupId)
                                 .ToListAsync();
        }
        public async Task<Homework> DeleteAsync(int id)
        {
            var homeworkModel = await _context.Homeworks.Include(h => h.HomeworkFiles).FirstOrDefaultAsync(h => h.Id == id);
            if (homeworkModel == null) throw new InvalidOperationException("Домашнее задание не найдено");

            _context.HomeworkFiles.RemoveRange(homeworkModel.HomeworkFiles);
            _context.Homeworks.Remove(homeworkModel);
            await _context.SaveChangesAsync();

            return homeworkModel;
        }
    }
}
