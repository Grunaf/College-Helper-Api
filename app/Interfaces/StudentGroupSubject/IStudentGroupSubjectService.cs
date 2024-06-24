using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupSubjectService
    {
        Task SyncGroupSubjectsFromScheduleAsync(int studentGroupId, Dictionary<string, Subject> subjectCache);
        public Task<bool> HasAnySubjectForStudentGroup(int headBoyChatId);
    }
}
