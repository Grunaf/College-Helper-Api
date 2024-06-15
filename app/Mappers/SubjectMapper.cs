using app.Dtos.Subject;
using app.Models;

namespace app.Mappers
{
    public static class SubjectMapper
    {
        public static GetSubjectRequestDto ToGetSubjectRequestDto(this Subject subjectModel)
        {
            return new GetSubjectRequestDto
            {
                Id = subjectModel.Id,
                Title = subjectModel.Title
            };
        }
    }
}
