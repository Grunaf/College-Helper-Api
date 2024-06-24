using app.Dtos.Student;
using app.Exceptions;
using app.Interfaces;
using app.Mappers;
using app.Models;
using System.Text.RegularExpressions;

namespace app.Services
{
    public class StudentGroupService(IStudentGroupRepository studentGroupRepository, IStudentService studentService) : IStudentGroupService
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IStudentGroupRepository _groupRepository = studentGroupRepository;

        public async Task<StudentGroup> GetGroupByHeadBoyChatIdAsync(long headBoyChatId)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);

            if (headBoy.IsHeadBoy)
            {
                return await _groupRepository.GetByIdAsync(headBoy.StudentGroupId);
            }

            throw new NotHeadBoyException("Пользователь не является старостой");
        }

        public async Task<StudentGroup> GetGroupByStudentChatIdAsync(long studentChatId)
        {
            var student = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            return await _groupRepository.GetByIdAsync(student.StudentGroupId);
        }
        public async Task<StudentGroup> GetByStudentChatIdAsync(long studentChatId)
        {
            var student = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            return await _groupRepository.GetByIdAsync(student.StudentGroupId);
        }
        public async Task CheckIsStudentInSomeGroupOrThrowExceptionAsync(int studentId, int studentGroupId)
        {
            var studentModel = await _studentService.GetByIdAsync(studentId);
            if (studentModel.StudentGroupId != studentGroupId) throw new StudentNotInGroupException("Студент не из твоей группы");
        }
        public async Task CheckIsStudentsInGroupOrThrowExceptionAsync(int studentGroupId)
        {
            if (!await _groupRepository.CheckIfStudentsExceptHeadBoyExistInGroupAsync(studentGroupId)) throw new DataNotFoundException("В группе кроме вас никого. Добавьте студентов в группу");
        }

        public async Task<List<GetStudentRequestDto>> GetAllStudentsExceptHeadBoyInGroupFromHeadBoy(long headBoyChatId)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            var students = await _studentService.GetStudentsByStudentGroupIdAsync(headBoy.StudentGroupId);

            return students.Where(s => s.ChatId != headBoyChatId).Select(s => s.ToGetStudentDtoFromModel()).ToList();
        }
        public async Task<List<StudentDto>> AddStudentsToGroupAsync(long headBoyChatId, List<StudentDto> studentsDto)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);

            foreach (var student in studentsDto)
            {
                await _studentService.CheckAndThrowIfStudentExistsAsync(student.ChatId);
                var studentModel = student.ToStudentFromCreateStudentDto(headBoy.StudentGroupId);
                await _studentService.CreateAsync(studentModel);
            }
            return studentsDto;
        }
        public async Task<StudentDto> UpdateStudentInGroupAsync(long headBoyChatId, int studentId, StudentDto studentDto)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);

            await CheckIsStudentInSomeGroupOrThrowExceptionAsync(studentId, headBoy.StudentGroupId);
            await _studentService.UpdateAsync(studentId, studentDto.ToStudentFromCreateStudentDto(headBoy.StudentGroupId));
            return studentDto;
        }
        public async Task<Student> DeleteStudentInGroupAsync(long headBoyChatId, long studentChatId)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);

            var studentModel = await _studentService.GetByChatIdAsync(studentChatId) ?? throw new DataNotFoundException("Студент не найден");
            if (studentModel.StudentGroupId != headBoy.StudentGroupId) throw new StudentNotInGroupException("Студент не из твоей группы");
            return await _studentService.DeleteAsync(studentModel.Id);
        }
    }
}
