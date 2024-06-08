using app.Models;

namespace app.Interfaces
{
    public interface ISheduleRepository
    {
        public Task<SheduleDay> CreateSheduleForDayAsync(SheduleDay sheduleDay);
        public Task<List<SheduleDaySubject>> GetNextSheduleDayByStudentChatIdAsync(int studentGroupId, byte week, byte day);
    }
}
