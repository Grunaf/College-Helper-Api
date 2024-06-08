using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace app.Repository
{
    public class SheduleRepository : ISheduleRepository
    {
        private readonly ApplicationContext _context;
        public SheduleRepository(ApplicationContext context)
        {
            _context = context;
        }
    
        public async Task<SheduleDay> CreateSheduleForDayAsync(SheduleDay sheduleDay)
        {
            await _context.SheduleDays.AddAsync(sheduleDay);
            await _context.SaveChangesAsync();
            return sheduleDay;
        }

        public async Task<List<SheduleDaySubject>> GetNextSheduleDayByStudentChatIdAsync(int studentGroupId, byte week, byte day)
        {
            var nextSheduleDay = await _context.SheduleDays
                                        .Include(sd => sd.SheduleDaySubjects)
                                        .ThenInclude(sds => sds.Subject)
                                        .Where(sd => sd.CountWeek == week && sd.CountDay == day + 1)
                                        .FirstOrDefaultAsync();

            if (nextSheduleDay == null)
            {
                var nextWeek = (byte)(week + 1 == 3 ? 1 : 2);
                nextSheduleDay = await _context.SheduleDays
                                        .Include(sd => sd.SheduleDaySubjects)
                                        .ThenInclude(sds => sds.Subject)
                                        .Where(sd => sd.CountWeek == nextWeek && sd.CountDay == 1)
                                        .FirstOrDefaultAsync();
            }
            return nextSheduleDay?.SheduleDaySubjects.OrderBy(s => s.Spot).ToList() ?? throw new InvalidOperationException(); 
        }
    }
}
