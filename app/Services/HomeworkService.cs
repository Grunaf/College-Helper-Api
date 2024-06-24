using app.Dtos;
using app.Dtos.Subject;
using app.Exceptions;
using app.Interfaces;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class HomeworkService(IStudentService studentService,
        IHomeworkRepository homeworkRepository,
        IHomeworkFilesRepository homeworkFilesRepository) : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepo = homeworkRepository;
        private readonly IHomeworkFilesRepository _homeworkFileRepo = homeworkFilesRepository;
        private readonly IStudentService _studentService = studentService;

        public async Task<CreateHomeworkRequestDto> AddByHeadBoyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            var headBoyModel = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            return await AddByStudentGroupIdAsync(headBoyModel.StudentGroupId, createHomeworkRequestDto);
        }
        private async Task<CreateHomeworkRequestDto> AddByStudentGroupIdAsync(int studentGroupId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            var homeworkFileModels = await CreateHomeworkFileModels(studentGroupId, createHomeworkRequestDto.FileIds);
            var homeworkModel = CreateHomeworkModel(studentGroupId, homeworkFileModels, createHomeworkRequestDto);
            await _homeworkRepo.CreateAsync(homeworkModel);

            return createHomeworkRequestDto;
        }

        public async Task<GetFullHomeworkRequestDto> GetByIdByStudentChatIdAsync(long studentChatId, int homeworkId)
        {
            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId); //Проверка есть ли в бд

            var homework = await _homeworkRepo.GetByIdAsync(homeworkId);
            return homework.ToGetFullHomeworkRequestDtoFromHomeworkModel();
        }
/*
        public async Task<List<GetSubjectRequestDto>?> GetSubjectsWhereIfHomework(long studentChatId, int subjectId)
        {
            var studentModel = await _studentService.GetStudentAsync(studentChatId);
            var homeworks = await _homeworkRepo.GetBySubjectIdAndGroupIdIncludeSubjectAsync(subjectId, studentModel.StudentGroupId);

            return homeworks.Select(h => h.Subject.ToGetSubjectRequestDto()).Distinct().ToList();
        }*/
        public async Task<List<GetHomeworkRequestDto>> GetBySubjectIdAndStudentChatIdAsync(long studentChatId, int subjectId)
        {
            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            var homeworks = await _homeworkRepo.GetBySubjectIdAndStudentChatIdAsync(subjectId, studentModel.StudentGroupId);

            return homeworks.Select(h => h.ToGetHomeworkRequestDtoFromHomeworkModel()).ToList();
        }

        public async Task<CreateHomeworkRequestDto> UpdateByHeadBoyChatIdAsync(long headBoyChatId, int homeworkId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            var headBoyModel = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            await _homeworkRepo.DeleteAsync(homeworkId);

            return await AddByStudentGroupIdAsync(headBoyModel.StudentGroupId, createHomeworkRequestDto);
        }

        private async Task<List<HomeworkFile>> CreateHomeworkFileModels(int groupId, List<string> fileIds)
        {
            var homeworkFiles = await _homeworkFileRepo.GetAllByStudentGroupIdAsync(groupId);

            List<HomeworkFile> homeworkFileModels = [];

            foreach (var fileId in fileIds)
            {

                bool fileExists = homeworkFiles.Any(hf => hf.FileId == fileId);
                if (!fileExists)
                {
                    homeworkFileModels.Add(new HomeworkFile
                    {
                        FileId = fileId
                    });
                }
                else
                {
                    throw new InvalidOperationException($"Этот файл уже используется в другом домашнем задании. Выберите другой файл.");
                }
            }
            return homeworkFileModels;
        }
        public async Task<List<Homework>> GetHomeworksForSubjects(List<int> subjectIds, int studentGroupId)
        {
            return await _homeworkRepo.GetHomeworksForSubjectsByStudentGroupIdAsync(subjectIds, studentGroupId);
        }
        private Homework CreateHomeworkModel(int studentGroupId, List<HomeworkFile> homeworkFiles, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            return new Homework
            {
                StudentGroupId = studentGroupId,
                SubjectId = createHomeworkRequestDto.SubjectId,
                Title = createHomeworkRequestDto.Title,
                Comment = createHomeworkRequestDto.Comment,
                HomeworkFiles = homeworkFiles
            };
        }
    }
}
