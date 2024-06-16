using app.Dtos.StudentAbsence;
using app.Interfaces;
using app.Interfaces.StudentAbsense;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class StudentAbsenceService(IStudentAbsenceRepository studentAbsenceRepo, IStudentGroupService studentGroupService) : IStudentAbsenceService
    {
        private readonly IStudentAbsenceRepository _studentAbsenceRepo = studentAbsenceRepo;
        private readonly IStudentGroupService _studentGroupService = studentGroupService;

        public async Task<ResultCreateOrDelete> CreateOrDeleteAsync(CreateOrDeleteStudentAbsenceRequestDto absenceDto)
        {
            if (absenceDto.AbsenceId != -1)
            {
                var deletedAbsence = await _studentAbsenceRepo.DeleteByIdAsync(absenceDto.AbsenceId.Value);
                return new ResultCreateOrDelete { 
                    StudentAbsence = deletedAbsence,
                    OperationType = OperationType.Delete
                };
            }
            var createdAbsence = await _studentAbsenceRepo.CreateAsync(absenceDto.ToStudentAbsenceFromCreateOrDeleteDto());
            return new ResultCreateOrDelete
            {
                StudentAbsence = createdAbsence,
                OperationType = OperationType.Create
            };
        }
        public async Task<List<StatStudentAbsenceRequestDto>> GetStatStudentAbsensesByHeadBoyChatIdAsync(long headBoyChatId)
        {
            var studentGroupModel = await _studentGroupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);
            var statStudentAbsenses = await _studentAbsenceRepo.GetStatStudentAbsencesByStudentGroupIdAsync(studentGroupModel.Id);
            var statStudentAbsensesDtos = statStudentAbsenses.GroupBy(sa => sa.StudentId).
                                            Select(group => new StatStudentAbsenceRequestDto
                                            {
                                                Name = group.First().Student.Name,
                                                Surname = group.First().Student.Surname,
                                                Patronymic = group.First().Student.Patronymic,
                                                CountAbsence = group.Count()
                                            }).ToList();
            return statStudentAbsensesDtos;
        }
    }
    public struct ResultCreateOrDelete
    {
        public StudentAbsence StudentAbsence { get; set; }
        public OperationType OperationType { get; set; }
    }
    public enum OperationType { Create, Delete }
}
