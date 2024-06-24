using app.Dtos.StudentAbsence;
using app.Models;
using app.Services;

namespace app.Interfaces.StudentAbsense
{
    public interface IStudentAbsenceService
    {
        Task<CreateOrDeleteStudentAbsenceRequestDto?> CreateOrDeleteAsync(long headBoyChatId, CreateOrDeleteStudentAbsenceRequestDto absenceDto);
        Task<GetCountsOfStudentsGroupAbsenceRequestDto> GetAbsencesCountForPeriodByHeadboyChatId(long headBoyChatId);
        Task<List<StudentAbsenceDto>> GetListAttendanceOnPairAsync(long headBoyChatId, DateTime date, byte lessonNumber);
    }
}
