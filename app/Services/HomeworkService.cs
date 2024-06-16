using app.Dtos;
using app.Interfaces;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class HomeworkService(IStudentGroupService studentGroupService,
        IHomeworkRepository homeworkRepository,
        IHomeworkFilesRepository homeworkFilesRepository) : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepo = homeworkRepository;
        private readonly IHomeworkFilesRepository _homeworkFileRepo = homeworkFilesRepository;
        private readonly IStudentGroupService _groupService = studentGroupService;

        public async Task<CreateHomeworkRequestDto> AddByHeadBoyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            var groupModel = await _groupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);
            var homeworkFileModels = await CreateHomeworkFileModels(groupModel.Id, createHomeworkRequestDto.FileIds);
            var homeworkModel = CreateHomeworkModel(groupModel, homeworkFileModels, createHomeworkRequestDto);
            await _homeworkRepo.CreateAsync(homeworkModel);

            return createHomeworkRequestDto;
        }

        public async Task<GetFullHomeworkRequestDto> GetByIdAsync(int homeworkId)
        {
            var homework = await _homeworkRepo.GetByIdAsync(homeworkId);
            return homework.ToGetFullHomeworkRequestDtoFromHomeworkModel();
        }

        public async Task<List<GetHomeworkRequestDto>?> GetBySubjectIdAndHeadBoyChatIdAsync(long headBoyChatId, int subjectId)
        {
            var groupModel = await _groupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);
            var homeworks = await _homeworkRepo.GetBySubjectIdAndGroupIdAsync(subjectId, groupModel.Id);
            return homeworks.Select(h => h.ToGetHomeworkRequestDtoFromHomeworkModel()).ToList();
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
        private Homework CreateHomeworkModel(StudentGroup groupModel, List<HomeworkFile> homeworkFiles, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            return new Homework
            {
                StudentGroup = groupModel,
                SubjectId = createHomeworkRequestDto.SubjectId,
                Title = createHomeworkRequestDto.Title,
                Comment = createHomeworkRequestDto.Comment,
                HomeworkFiles = homeworkFiles
            };
        }
    }
}
