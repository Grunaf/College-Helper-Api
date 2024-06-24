using app.Dtos;
using app.Models;

namespace app.Mappers
{
    public static class HomeworkMappers
    {
        public static GetHomeworkRequestDto ToGetHomeworkRequestDtoFromHomeworkModel(this Homework homework)
        {
            return new GetHomeworkRequestDto
            {
                Id = homework.Id,
                Title = homework.Title,
                CreatedTime = homework.CreatedTime
            };
        }
        public static GetHomeworkWithSubjectTitleRequestDto ToGetHomeworkWithSubjectTitleRequestDtoFromHomeworkModel(this Homework homework)
        {
            return new GetHomeworkWithSubjectTitleRequestDto
            {
                Id = homework.Id,
                Topic = homework.Title,
                SubjectTitle = homework.Subject.Title,
                CreatedTime = homework.CreatedTime
            };
        }
        public static GetFullHomeworkRequestDto ToGetFullHomeworkRequestDtoFromHomeworkModel(this Homework homework)
        {
            return new GetFullHomeworkRequestDto
            {
                Title = homework.Title,
                Comment = homework.Comment,
                CreatedTime = homework.CreatedTime,
                FileIds = homework.HomeworkFiles.Select(hf => hf.FileId).ToList()
            };
        }
    }
}
