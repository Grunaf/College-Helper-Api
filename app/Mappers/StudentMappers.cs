using app.Dtos.Student;
using app.Models;

namespace app.Mappers
{
    public static class StudentMappers
    {
        public static StudentRoleInfoDto ToStudentRoleInfoDtoFromStudentModel(this Student studentModel)
        {
            return new StudentRoleInfoDto
            {
                IsHeadBoy = studentModel.IsHeadBoy
            };
        }
/*        public static GetStudentRequestDto ToUserDto(this Student studentModel)
        {
            return new GetStudentRequestDto
            {
                Id = studentModel.Id,
                IdChat = studentModel.ChatId,
                IsHeadBoy = studentModel.IsHeadBoy,
                Name = studentModel.Name,
                Patronymic = studentModel.Patronymic,
                Surname = studentModel.Surname,
*//*                StudentGroupId = userModel.StudentGroupId,
                StudentGroup = userModel.StudentGroup.ToStudentGroupDto()*//*
            };
        }*/
        public static Student ToStudentFromCreateStudentDto(this StudentDto studentDto, int studentGroupId)
        {
            return new Student
            {
                ChatId = studentDto.ChatId,
                IsHeadBoy = studentDto.IsHeadBoy,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Patronymic = studentDto.Patronymic,
                StudentGroupId = studentGroupId
            };
        }
        public static GetStudentRequestDto ToGetStudentDtoFromModel(this Student studentModel)
        {
            return new GetStudentRequestDto
            {
                Id = studentModel.Id,
                ChatId = studentModel.ChatId,
                Name = studentModel.Name,
                Surname = studentModel.Surname,
                Patronymic = studentModel.Patronymic,
            };
        }
        public static Student ToUserFromUpdateDto(this UpdateStudentRequestDto studentDto)
        {
            return new Student
            {
                ChatId = studentDto.IdChat,
                IsHeadBoy = studentDto.IsHeadBoy,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Patronymic = studentDto.Patronymic,
                StudentGroupId = studentDto.StudentGroupId
            };
        }
/*        public static StudentAttendanceDto ToUserAttendanceDto(this Student studentModel)
        {
            return new StudentAttendanceDto
            {
                Id = studentModel.Id,
                Name = studentModel.Name,
                Surname = studentModel.Surname,
                Patronymic = studentModel.Patronymic
            };
        }*/
    }
}
