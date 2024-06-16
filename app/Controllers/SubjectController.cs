using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepository, IStudentRepository studentRepository, IStudentGroupService studentGroupService, ILogger<SubjectController> logger) : ControllerBase
    {
        private readonly IStudentRepository _studentRepo = studentRepository;
        private readonly ISubjectRepository _subjectRepo = subjectRepository;
        private readonly IStudentGroupService _groupService = studentGroupService;
        private readonly ILogger<SubjectController> _logger = logger;

        [HttpGet("{headBoyChatId}")]
        public async Task<IActionResult> GetAllByHeadBoyChatId(long headBoyChatId)
        {
            try
            {
                var groupModel = await _groupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);
                var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(groupModel.Id);

                return Ok(subjectModels.Select(s => s.ToGetSubjectRequestDto()));
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
        }
    }
}
