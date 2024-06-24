using app.Exceptions;
using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepository, IStudentRepository studentRepository,
        IStudentGroupService studentGroupService, IStudentGroupSubjectService studentGroupSubjectService,
        ILogger<SubjectController> logger) : ControllerBase
    {
        private readonly IStudentRepository _studentRepo = studentRepository;
        private readonly ISubjectRepository _subjectRepo = subjectRepository;
        private readonly IStudentGroupService _groupService = studentGroupService;
        private readonly IStudentGroupSubjectService _groupSubjectService = studentGroupSubjectService;
        private readonly ILogger<SubjectController> _logger = logger;

        /*
        [HttpGet("group/{studentChatId}/whereIsHomeworks/")]
        public async Task<IActionResult> GetSubjectWhereIsHomeworkByStudentChatIdAsync(long studentChatId)
        {
            try
            {
                var groupModel = await _groupService.GetGroupByStudentChatIdAsync(studentChatId);
                var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(groupModel.Id);
                if (subjectModels.Count == 0) throw new DataNotFoundException("Расписание не добавлено");

                //var homeworkSubjects = subjectModels.Where(s => s.Homeworks.Any(h => h.StudentGroupId == groupModel.Id)).Select.ToList();
                var homeworks = await _subjectRepo.GetAllSubjectWhereIsHomeworkByStudentGroupIdAsync(groupModel.Id);

                if (homeworks.Count == 0) return NoContent();
                return Ok(homeworks.Select(s => s.ToGetSubjectRequestDto()));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{studentChatId}")]
        public async Task<IActionResult> GetAllActualByStudentChatId(long studentChatId)
        {
            try
            {
                var groupModel = await _groupService.GetGroupByStudentChatIdAsync(studentChatId);
                var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(groupModel.Id);

                if (subjectModels.Count == 0) throw new DataNotFoundException("Расписание не добавлено");
                return Ok(subjectModels.Select(s => s.ToGetSubjectRequestDto()));
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("group/{headBoyChatId}/hasSubjects")]
        public async Task<IActionResult> HasAnySubjectForGroup(int headBoyChatId)
        {
            try
            {
                bool hasSubjects = await _groupSubjectService.HasAnySubjectForStudentGroup(headBoyChatId);

                return Ok(hasSubjects);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }*/
    }
}
