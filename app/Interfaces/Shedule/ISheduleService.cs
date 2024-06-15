using app.Dtos.SheduleDay;
using app.Dtos.Subject;

namespace app.Interfaces.Shedule
{
    public interface ISheduleService
    {
        public Task<CreateSheduleRequestDto> CreateSheduleAsync(long headBoyChatId, CreateSheduleRequestDto sheduleDto);
        public Task<GetSheduleDayRequestDto> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId);
    }
}
