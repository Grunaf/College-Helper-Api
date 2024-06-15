using app.Dtos.StudentGroup;
using app.Models;

namespace app.Mappers
{
    public static class StudentGroupMappers
    {
        public static StudentGroupDto ToStudentGroupDto(this StudentGroup groupModel)
        {
            return new StudentGroupDto
            {
                Id = groupModel.Id,
                Field = groupModel.Field.ToString(),
                Number = groupModel.Number,
                CuratorId = groupModel.CuratorId,
                //Curator = groupModel.Curator.ToUserDto(),
                //HeadBoy = groupModel.HeadBoy.ToUserDto(),
            };
        }
    }
}
