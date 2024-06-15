using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupSubjectService
    {
        public Task<StudentGroupSubject> AddIfMissingAsync(StudentGroupSubject studentGroupSubject);
        Task SyncGroupSubjectsFromScheduleAsync(int studentGroupId, Dictionary<string, Subject> subjectCache);
    }
}
