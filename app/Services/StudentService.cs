using app.Dtos.Student;
using app.Dtos.StudentAbsence;
using app.Exceptions;
using app.Interfaces;
using app.Interfaces.StudentAbsense;
using app.Mappers;
using app.Models;
using app.Repository;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace app.Services
{
    public class StudentService(IStudentRepository studentRepo) : IStudentService
    {
        private readonly IStudentRepository _studentRepo = studentRepo;

        public async Task<StudentRoleInfoDto> GetInfoByChatIdAsync(long chatId)
        {
            var student = await GetStudentOrThrowExceptionAsync(chatId);
            return student.ToStudentRoleInfoDtoFromStudentModel();
        }


        public async Task<Student> GetHeadBoyOrThrowExceptionAsync(long headBoyChatId)
        {
            var headBoy = await GetStudentOrThrowExceptionAsync(headBoyChatId);
            if (!headBoy.IsHeadBoy) throw new NotHeadBoyException("Пользователь не является старостой");
            return headBoy;
        }
        public async Task<Student> GetStudentOrThrowExceptionAsync(long studentChatId)
        {
            return await _studentRepo.GetByChatIdAsync(studentChatId) ?? throw new StudentNotFoundException("Похоже, я вас не знаю");
        }
        public async Task CheckAndThrowIfStudentExistsAsync(long studentChatId)
        {
            if (await _studentRepo.GetByChatIdAsync(studentChatId) != null)
            {
                throw new DataNotFoundException("Студент уже добавлен. Если он перешел из другой группы, пожалуйста, запросите предыдущего старосту удалить его из своей");
            }
        }

        public async Task<List<Student>> GetStudentsByStudentGroupIdAsync(int studentGroupId)
        {
            return await _studentRepo.GetStudentsByStudentGroupIdAsync(studentGroupId);
        }
        public async Task<Student> GetByIdAsync(int studentId)
        {
            return await _studentRepo.GetByIdAsync(studentId);
        }
        public async Task<Student?> GetByChatIdAsync(long chatId)
        {
            return await _studentRepo.GetByChatIdAsync(chatId);
        }
        public async Task<Student> CreateAsync(Student student)
        {
            return await _studentRepo.CreateAsync(student);
        }
        public async Task<Student> UpdateAsync(int studentId, Student student)
        {
            return await _studentRepo.UpdateAsync(studentId, student);
        }
        public async Task<Student> DeleteAsync(int studentId)
        {
            return await _studentRepo.DeleteAsync(studentId);
        }
    }
}
