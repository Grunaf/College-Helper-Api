using app.Dtos.SheduleDay;
using app.Dtos.Subject;

namespace app.Interfaces
{
    public interface ISheduleService
    {
        public Task<CreateSheduleRequestDto> CreateSheduleAsync(long headBoyChatId, CreateSheduleRequestDto sheduleDto);
        public Task<List<SheduleDaySubjectDto>> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId);
    }
}
