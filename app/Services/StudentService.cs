using app.Dtos.StudentAbsence;
using app.Interfaces;
using app.Interfaces.StudentAbsense;
using app.Models;
using System.Xml.Linq;

namespace app.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IStudentAbsenceRepository _studentAbsenceRepo;
        private readonly IStudentGroupService _studentGroupService;
        public StudentService(IStudentRepository studentRepo, IStudentAbsenceRepository studentAbsenceRepo, IStudentGroupService studentGroupService)
        {
            _studentRepo = studentRepo;
            _studentAbsenceRepo = studentAbsenceRepo;
            _studentGroupService = studentGroupService;
        }

        public async Task<bool> IsStudentHeadBoyByChatIdAsync(long chatId)
        {
            try
            {
                var student = await _studentRepo.GetByChatIdAsync(chatId);
                return student.IsHeadBoy;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<StudentAbsenceDto>> GetStatusStudentFromListAttendanceAsync(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            List<StudentAbsenceDto> studentAttendanceDtos = [];
            var students = await _studentRepo.GetStudentsByHeadBoyChatIdAsync(headBoyChatId);
            var studentGroupModel = await _studentGroupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);
            var absence = await _studentAbsenceRepo.GetAllByStudentGroupIdAsync(studentGroupModel.Id, date, lessonNumber);

            foreach (var student in students)
            {
                var existingAbsence = absence.FirstOrDefault(a => a.StudentId == student.Id);

                var studentAbsenceDto = new StudentAbsenceDto
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    Id = existingAbsence?.Id ?? -1, // Если existingAbsence != null, то Id будет existingAbsence.Id, иначе 0
                    OnPair = existingAbsence == null
                };

                studentAttendanceDtos.Add(studentAbsenceDto);
            }
            return studentAttendanceDtos;
        }
    }
}
