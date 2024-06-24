using app.Dtos.Subject;
using app.Exceptions;
using app.Interfaces;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class SubjectService(IStudentService studentService, ISubjectRepository subjectRepository) : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepo = subjectRepository;
        private readonly IStudentService _studentService = studentService;
        public async Task<List<GetSubjectRequestDto>> GetSubjectsWithHomeworkByStudentChatIdAsync(long studentChatId)
        {
            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(studentModel.StudentGroupId);
            if (subjectModels.Count == 0) throw new DataNotFoundException("Расписание не добавлено");

            var subjectWithHomeworks = await _subjectRepo.GetSubjectsWithHomeworksByStudentGroupIdAsync(subjectModels.Select(s => s.Id).ToList(), studentModel.StudentGroupId);

            if (subjectWithHomeworks.Count == 0) throw new DataNotFoundException("Нет домашнего задания");
            return subjectWithHomeworks.Select(s => s.ToGetSubjectRequestDto()).ToList();
        }
        public async Task<List<GetSubjectRequestDto>> GetActualSubjectsByStudentChatIdAsync(long studentChatId)
        {
            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(studentModel.StudentGroupId);
            if (subjectModels.Count == 0) throw new DataNotFoundException("Расписание не добавлено");

            return subjectModels.Select(s => s.ToGetSubjectRequestDto()).ToList();
        }
    }
}
