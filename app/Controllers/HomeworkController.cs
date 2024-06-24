using app.Dtos;
using app.Exceptions;
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
        private readonly IHomeworkService _homeworkService = homeworkService;
        private readonly ILogger<HomeworkController> _logger = logger;
/*
        [HttpPost]
        public async Task<IActionResult> AddByHeadBoyChatId([FromQuery] long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            try
            {
                var homework = await _homeworkService.AddByHeadBoyChatIdAsync(headBoyChatId, createHomeworkRequestDto);
                return Ok(homework);
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
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
        }*/
    }
}
