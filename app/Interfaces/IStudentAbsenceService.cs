using app.Dtos.StudentAbsence;
using app.Models;
using app.Services;

namespace app.Interfaces
{
    public interface IStudentAbsenceService
    {
        public Task<ResultCreateOrDelete> CreateOrDeleteAsync(CreateOrDeleteStudentAbsenceRequestDto absenceDto);
        public Task<List<StatStudentAbsenseRequestDto>> GetStatStudentAbsensesRequestDtosAsync(long headBoyChatId);
    }
}
