using app.Dtos.StudentAbsence;
using app.Interfaces;
using app.Models;
using System.Xml.Linq;

namespace app.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IStudentAbsenceRepository _studentAbsenceRepo;
        public StudentService(IStudentRepository studentRepo, IStudentAbsenceRepository studentAbsenceRepo)
        {
            _studentRepo = studentRepo;
            _studentAbsenceRepo = studentAbsenceRepo;
        }

        public async Task<List<StudentAbsenceDto>> GetStatusStudentFromListAttendanceAsync(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            List<StudentAbsenceDto> studentAttendanceDtos = new();
            var students = await _studentRepo.GetStudentsByHeadBoyChatIdAsync(headBoyChatId);
            var absence = await _studentAbsenceRepo.GetAllByHeadBoyChatIdAsync(headBoyChatId, date, lessonNumber);

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
