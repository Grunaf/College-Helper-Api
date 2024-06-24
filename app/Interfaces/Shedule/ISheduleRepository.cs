using app.Models;

namespace app.Interfaces.Shedule
{
    public interface ISheduleRepository
    {
        Task<SheduleDay> CreateSheduleForDayAsync(SheduleDay sheduleDay);
        Task<bool> CheckIfExistsSheduleDaysByStudentGroupIdAsync(int studentGroupId);
        Task<List<SheduleDay>?> DeleteSheduleIfExistsByStudentGroupIdAsync(int studentGroupId);
        Task<SheduleDay> GetNextSheduleDayByStudentGroupIdAsync(int studentGroupId, byte week, byte day);
        Task<List<SheduleDay>> GetAllByStudentGroupId(int studentGroupId);
    }
}
