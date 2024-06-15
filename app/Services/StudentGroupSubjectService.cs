using app.Interfaces;
using app.Models;

namespace app.Services
{
    public class StudentGroupSubjectService(IStudentGroupSubjectRepository groupRepository,
                        ISubjectRepository subjectRepository) : IStudentGroupSubjectService
    {
        private readonly IStudentGroupSubjectRepository _groupSubjectRepo = groupRepository;
        private readonly ISubjectRepository _subjectRepo = subjectRepository;

        public async Task<StudentGroupSubject> AddIfMissingAsync(StudentGroupSubject studentGroupSubject)
        {
            var studentGroupSubjectFinded = await _groupSubjectRepo.GetBySubjectIdAsync(studentGroupSubject.SubjectId);
            if (studentGroupSubjectFinded == null)
            {
                return await _groupSubjectRepo.CreateAsync(studentGroupSubject);
            }
            return studentGroupSubjectFinded;
        }
        
        public async Task SyncGroupSubjectsFromScheduleAsync(int studentGroupId, Dictionary<string, Subject> subjectCache)
        {
            var groupSubjects = await _subjectRepo.GetAllByStudentGroupIdAsync(studentGroupId);

            foreach (var subject in groupSubjects)
            {
                if (!subjectCache.ContainsKey(subject.Title))
                {
                    await _groupSubjectRepo.UpdateAsync(new StudentGroupSubject
                    {
                        SubjectId = subject.Id,
                        StudentGroupId = studentGroupId,
                        IsExpired = true,
                    });
                }
            }

            foreach (var kvp in subjectCache.Where(kvp => !groupSubjects.Any(s => s.Title == kvp.Key)))
            {
                var subject = kvp.Value;
                var studentGroupSubject = new StudentGroupSubject
                {
                    SubjectId = subject.Id,
                    StudentGroupId = studentGroupId
                };

                await _groupSubjectRepo.CreateAsync(studentGroupSubject);
            }
        }
    }
}
