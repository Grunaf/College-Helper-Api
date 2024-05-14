﻿using app.Dtos.Student;
using app.Models;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace app.Mappers
{
    public static class StudentMappers
    {
        public static StudentDto ToUserDto(this Student studentModel)
        {
            return new StudentDto
            {
                Id = studentModel.Id,
                IdChat = studentModel.IdChat,
                IsHeadBoy = studentModel.IsHeadBoy,
                Name = studentModel.Name,
                Patronymic = studentModel.Patronymic,
                Surname = studentModel.Surname,
/*                StudentGroupId = userModel.StudentGroupId,
                StudentGroup = userModel.StudentGroup.ToStudentGroupDto()*/
            };
        }
        public static Student ToUserFromCreateDto(this CreateStudentRequestDto studentDto)
        {
            return new Student
            {
                IdChat = studentDto.IdChat,
                IsHeadBoy = studentDto.IsHeadboy,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Patronymic = studentDto.Patronymic,
                StudentGroupId = studentDto.StudentGroupId
            };
        }
        public static Student ToUserFromUpdateDto(this UpdateStudentRequestDto studentDto)
        {
            return new Student
            {
                IdChat = studentDto.IdChat,
                IsHeadBoy = studentDto.IsHeadboy,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Patronymic = studentDto.Patronymic,
                StudentGroupId = studentDto.StudentGroupId
            };
        }
        public static StudentAttendanceDto ToUserAttendanceDto(this Student studentModel)
        {
            return new StudentAttendanceDto
            {
                Id = studentModel.Id,
                Name = studentModel.Name,
                Surname = studentModel.Surname,
                Patronymic = studentModel.Patronymic
            };
        }
    }
}
