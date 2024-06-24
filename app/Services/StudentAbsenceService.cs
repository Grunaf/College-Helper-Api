using app.Dtos.StudentAbsence;
using app.Exceptions;
using app.Interfaces;
using app.Interfaces.StudentAbsense;
using app.Mappers;
using app.Models;
using System.Diagnostics.Eventing.Reader;

namespace app.Services
{
    public class StudentAbsenceService(IStudentAbsenceRepository studentAbsenceRepo, IStudentGroupService studentGroupService, IStudentService studentService) : IStudentAbsenceService
    {
        private readonly IStudentAbsenceRepository _studentAbsenceRepo = studentAbsenceRepo;
        private readonly IStudentGroupService _studentGroupService = studentGroupService;
        private readonly IStudentService _studentService = studentService;

        public async Task<CreateOrDeleteStudentAbsenceRequestDto?> CreateOrDeleteAsync(long headBoyChatId, CreateOrDeleteStudentAbsenceRequestDto absenceDto)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            await _studentGroupService.CheckIsStudentInSomeGroupOrThrowExceptionAsync(absenceDto.StudentId, headBoy.StudentGroupId);

            var absenceModel = await _studentAbsenceRepo.GetByStudentIdAndLessonNumberAsync(absenceDto.StudentId, absenceDto.LessonNumber, DateTime.Today);
            if (absenceModel == null)
            {
                await _studentAbsenceRepo.CreateAsync(absenceDto.ToStudentAbsence());
                return absenceDto;
            }

            await _studentAbsenceRepo.DeleteByIdAsync(absenceModel.Id);
            return null;
        }

        
        public async Task<List<StudentAbsenceDto>> GetListAttendanceOnPairAsync(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            List<StudentAbsenceDto> studentAttendanceDtos = [];

            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            await _studentGroupService.CheckIsStudentsInGroupOrThrowExceptionAsync(headBoy.StudentGroupId);

            var students = await _studentService.GetStudentsByStudentGroupIdAsync(headBoy.StudentGroupId);
            var absence = await _studentAbsenceRepo.GetAllByStudentGroupIdAsync(headBoy.StudentGroupId, date, lessonNumber);

            foreach (var student in students)
            {
                var existingAbsence = absence.FirstOrDefault(a => a.StudentId == student.Id);

                var studentAbsenceDto = new StudentAbsenceDto
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    Id = existingAbsence?.Id,
                    OnPair = existingAbsence == null
                };

                studentAttendanceDtos.Add(studentAbsenceDto);
            }
            return studentAttendanceDtos;
        }

        public async Task<GetCountsOfStudentsGroupAbsenceRequestDto> GetAbsencesCountForPeriodByHeadboyChatId(long headBoyChatId)
        {
            var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
            await _studentGroupService.CheckIsStudentsInGroupOrThrowExceptionAsync(headBoy.StudentGroupId);

            DateTime currentDate = DateTime.Today;
            DateTime fromDate, toDate;

            if (currentDate.Month >= 6 && currentDate.Month < 9)
            {
                fromDate = new DateTime(currentDate.Year, 1, 1);
                toDate = new DateTime(currentDate.Year, 6, 1);
            }
            else if (currentDate.Month >= 1 && currentDate.Month < 6)
            {
                fromDate = new DateTime(currentDate.Year - 1, 9, 1);
                toDate = new DateTime(currentDate.Year, 1, 1);
            }
            else throw new DataNotFoundException("Данные за предыдущий семестр не готовы.");

            var statStudentAbsenses = await _studentAbsenceRepo.GetAbsencesCountForPeriodByStudentGroupId(headBoy.StudentGroupId, fromDate, toDate);
            var statStudentAbsensesDtos = statStudentAbsenses.GroupBy(sa => sa.StudentId).
                                            Select(group => new CountStudentAbsencesRequestDto
                                            {
                                                Name = group.First().Student.Name,
                                                Surname = group.First().Student.Surname,
                                                Patronymic = group.First().Student.Patronymic,
                                                CountAbsence = group.Count()
                                            }).ToList();
            var countsOfStudentGroupAbsences = new GetCountsOfStudentsGroupAbsenceRequestDto { StartOfSemester = fromDate, EndOfSemester = toDate,
                                                                                            countStudentAbsencesRequestDtos = statStudentAbsensesDtos };
            return countsOfStudentGroupAbsences;
        }
    }
}
