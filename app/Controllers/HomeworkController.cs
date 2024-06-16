using app.Dtos;
using app.Interfaces;
using app.Interfaces.Shedule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [Route("api/homeworks")]
    [ApiController]
    public class HomeworkController(IHomeworkService homeworkService, ILogger<HomeworkController> logger) : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly ILogger<HomeworkController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> AddByHeadBoyChatId([FromQuery] long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            try
            {
                var homework = await _homeworkService.AddByHeadBoyChatIdAsync(headBoyChatId, createHomeworkRequestDto);
                return Ok(homework);
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

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubjectIdAndHeadBoyChatIdAsync(int subjectId, [FromQuery] long headBoyChatId)
        {
            try
            {
                var homeworks = await _homeworkService.GetBySubjectIdAndHeadBoyChatIdAsync(headBoyChatId, subjectId);
                return Ok(homeworks);
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var homework = await _homeworkService.GetByIdAsync(id);
                return Ok(homework);
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
