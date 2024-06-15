using app.Dtos;

namespace app.Interfaces
{
    public interface IHomeworkService
    {
        public Task<CreateHomeworkRequestDto> AddByHeadboyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto);
        public Task<GetFullHomeworkRequestDto> GetByIdAsync(int homeworkId);
        public Task<List<GetHomeworkRequestDto>?> GetBySubjectIdAndHeadboyChatIdAsync(int subjectId, int headboyChatId);
    }
}
