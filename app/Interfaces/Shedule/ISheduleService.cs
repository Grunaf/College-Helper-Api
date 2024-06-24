using app.Dtos.SheduleDay;
using app.Dtos.Subject;

namespace app.Interfaces.Shedule
{
    public interface ISheduleService
    {
        public Task<SheduleRequestDto> CreateSheduleAsync(long headBoyChatId, SheduleRequestDto sheduleDto);
        public Task<GetSheduleDayRequestDto> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId);
        Task<SheduleRequestDto> GetSheduleByChatIdAsync(long studentChatId);
    }
}
