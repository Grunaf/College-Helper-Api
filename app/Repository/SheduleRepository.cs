using app.Interfaces.Shedule;
using app.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace app.Repository
{
    public class SheduleRepository(ApplicationContext context) : ISheduleRepository
    {
        private readonly ApplicationContext _context = context;

        public async Task<SheduleDay> CreateSheduleForDayAsync(SheduleDay sheduleDay)
        {
            await _context.SheduleDays.AddAsync(sheduleDay);
            await _context.SaveChangesAsync();
            return sheduleDay;
        }

        public async Task<List<SheduleDay>?> DeleteSheduleIfExistsByStudentGroupIdAsync(int studentGroupId)
        {
            var sheduleDays = await _context.SheduleDays.Include(sd => sd.SheduleDaySubjects)
                                        .Where(sd => sd.StudentGroup.Id == studentGroupId).ToListAsync();
            if (sheduleDays != null)
            {
                foreach (var sheduleDay in sheduleDays)
                {
                    _context.SheduleDaySubjects.RemoveRange(sheduleDay.SheduleDaySubjects);
                }
                _context.SheduleDays.RemoveRange(sheduleDays);
                await _context.SaveChangesAsync();
            }

            return sheduleDays;
        }

        public async Task<SheduleDay> GetNextSheduleDayByStudentChatIdAsync(int studentGroupId, byte week, byte day)
        {
            var nextSheduleDay = await _context.SheduleDays
                                        .Include(sd => sd.SheduleDaySubjects)
                                        .ThenInclude(sds => sds.Subject)
                                        .Where(sd => sd.StudentGroup.Id == studentGroupId && sd.CountWeek == week && sd.CountDay == day + 1)
                                        .FirstOrDefaultAsync();

            if (nextSheduleDay == null)
            {
                var nextWeek = (byte)(week + 1 == 3 ? 1 : 2);
                nextSheduleDay = await _context.SheduleDays
                                        .Include(sd => sd.SheduleDaySubjects)
                                        .ThenInclude(sds => sds.Subject)
                                        .Where(sd => sd.StudentGroup.Id == studentGroupId && sd.CountWeek == nextWeek && sd.CountDay == 1)
                                        .FirstOrDefaultAsync();
            }
            return nextSheduleDay ?? throw new InvalidOperationException("Не удалось получить следующий уч. день расписания"); 
        }
    }
}
