using app.Dtos;

namespace app.Interfaces
{
    public interface IHomeworkService
    {
        public Task<CreateHomeworkRequestDto> AddByHeadBoyChatIdAsync(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto);
        public Task<GetFullHomeworkRequestDto> GetByIdAsync(int homeworkId);
        public Task<List<GetHomeworkRequestDto>?> GetBySubjectIdAndHeadBoyChatIdAsync(long headBoyChatId, int subjectId);
    }
}
