using app.Models;

namespace app.Interfaces.Shedule
{
    public interface ISheduleRepository
    {
        public Task<SheduleDay> CreateSheduleForDayAsync(SheduleDay sheduleDay);
        public Task<List<SheduleDay>?> DeleteSheduleIfExistsByStudentGroupIdAsync(int studentGroupId);
        public Task<SheduleDay> GetNextSheduleDayByStudentChatIdAsync(int studentGroupId, byte week, byte day);
    }
}
