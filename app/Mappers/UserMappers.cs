using app.Dtos.User;
using app.Models;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace app.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                IdChat = userModel.IdChat,
                Role = userModel.Role.ToString(),
                Name = userModel.Name,
                Surname = userModel.Surname,
                StudentGroupId = userModel.StudentGroupId,
                StudentGroup = userModel.StudentGroup.ToStudentGroupDto()
            };
        }
        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
        {
            return new User
            {
                IdChat = userDto.IdChat,
                Role = (Role)Enum.Parse(typeof(Role), userDto.Role),
                Name = userDto.Name,
                Surname = userDto.Surname,
                Patronymic = userDto.Patronymic,
                StudentGroupId = userDto.StudentGroupId
            };
        }
        public static User ToUserFromUpdateDto(this UpdateUserRequestDto userDto)
        {
            return new User
            {
                IdChat = userDto.IdChat,
                Role = (Role)Enum.Parse(typeof(Role), userDto.Role),
                Name = userDto.Name,
                Surname = userDto.Surname,
                Patronymic = userDto.Patronymic,
                StudentGroupId = userDto.StudentGroupId
            };
        }
    }
}
