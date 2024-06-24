using app.Interfaces;
using app.Models;

namespace app.Services
{
    public class StudentGroupSubjectService(IStudentGroupSubjectRepository groupRepository,
                        ISubjectRepository subjectRepository, IStudentGroupService groupService) : IStudentGroupSubjectService
    {
        private readonly IStudentGroupSubjectRepository _groupSubjectRepo = groupRepository;
        private readonly IStudentGroupService _groupService = groupService;
        private readonly ISubjectRepository _subjectRepo = subjectRepository;

/*        public async Task<StudentGroupSubject> AddIfMissingAsync(StudentGroupSubject studentGroupSubject)
        {
            var studentGroupSubjectFinded = await _groupSubjectRepo.GetBySubjectIdAsync(studentGroupSubject.SubjectId);
            if (studentGroupSubjectFinded == null)
            {
                return await _groupSubjectRepo.CreateAsync(studentGroupSubject);
            }
            return studentGroupSubjectFinded;
        }
        */

        public async Task<bool> HasAnySubjectForStudentGroup(int studentChatId)
        {
            var groupModel = await _groupService.GetGroupByStudentChatIdAsync(studentChatId);

            var groupSubject = await _groupSubjectRepo.HasAnySubjectForStudentGroup(groupModel.Id);
            return groupSubject != null;
        }

        public async Task SyncGroupSubjectsFromScheduleAsync(int studentGroupId, Dictionary<string, Subject> subjectCache)
        {
            var groupSubjects = await _subjectRepo.GetAllByStudentGroupIdAsync(studentGroupId);

            foreach (var subject in groupSubjects)
            {
                var studentGroupSubjectModel = new StudentGroupSubject
                {
                    SubjectId = subject.Id,
                    StudentGroupId = studentGroupId
                };

                if (!subjectCache.ContainsKey(subject.Title))
                {
                    studentGroupSubjectModel.IsExpired = true;
                    await _groupSubjectRepo.UpdateAsync(studentGroupSubjectModel);
                }

                else await _groupSubjectRepo.UpdateAsync(studentGroupSubjectModel);
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
