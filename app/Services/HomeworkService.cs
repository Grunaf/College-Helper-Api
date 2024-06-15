using app.Dtos;
using app.Interfaces;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IStudentGroupRepository _groupRepo;
        private readonly IHomeworkRepository _homeworkRepo;
        private readonly IHomeworkFilesRepository _homeworkFileRepo;
        public HomeworkService(IStudentGroupRepository studentGroupRepository, IHomeworkRepository homeworkRepository, IHomeworkFilesRepository homeworkFilesRepository)
        {
            _groupRepo = studentGroupRepository;
            _homeworkRepo = homeworkRepository;
            _homeworkFileRepo = homeworkFilesRepository;
        }
        public async Task<CreateHomeworkRequestDto> AddByHeadboyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            try
            {
                var groupModel = await _groupRepo.GetByHeadBoyChatIdAsync(headBoyChatId);
                var homeworkFileModels = await CreateHomeworkFileModels(groupModel.Id, createHomeworkRequestDto.FileIds);
                var homeworkModel = CreateHomeworkModel(groupModel, homeworkFileModels, createHomeworkRequestDto);
                await _homeworkRepo.CreateAsync(homeworkModel);

                return createHomeworkRequestDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<GetFullHomeworkRequestDto> GetByIdAsync(int homeworkId)
        {
            try
            {
                var homework = await _homeworkRepo.GetByIdAsync(homeworkId);
                return homework.ToGetFullHomeworkRequestDtoFromHomeworkModel();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<GetHomeworkRequestDto>?> GetBySubjectIdAndHeadboyChatIdAsync(int subjectId, int headboyChatId)
        {
            try
            {
                var groupModel = await _groupRepo.GetByHeadBoyChatIdAsync(headboyChatId);
                var homeworks = await _homeworkRepo.GetBySubjectIdAndGroupIdAsync(subjectId, groupModel.Id);
                return homeworks.Select(h => h.ToGetHomeworkRequestDtoFromHomeworkModel()).ToList();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
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
