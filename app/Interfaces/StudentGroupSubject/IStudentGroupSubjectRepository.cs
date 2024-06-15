using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupSubjectRepository
    {
        public Task<StudentGroupSubject> CreateAsync(StudentGroupSubject studentGroupSubject);
        public Task<StudentGroupSubject?> GetBySubjectIdAsync(int subjectId);
        public Task<StudentGroupSubject?> UpdateAsync(StudentGroupSubject studentGroupSubject);

    }
}
