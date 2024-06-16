using app.Dtos.SheduleDay;
using app.Interfaces.Shedule;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/shedules")]
    [ApiController]
    public class SheduleController(ISheduleService sheduleService, ILogger<SheduleController> logger) : ControllerBase
    {
        private readonly ISheduleService _sheduleService = sheduleService;
        private readonly ILogger<SheduleController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> CreateByHeadBoyChatId([FromQuery] long headBoyChatId, CreateSheduleRequestDto sheduleDto)
        {
            try
            {
                var createdSheduleDto = await _sheduleService.CreateSheduleAsync(headBoyChatId, sheduleDto);
                return Ok(createdSheduleDto);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{studentChatId}")]
        public async Task<IActionResult> GetTommorowSheduleDayByChatId(long studentChatId)
        {
            try
            {
                var sheduleDaySubject = await _sheduleService.GetTommorowSheduleDayByStudentChatIdAsync(studentChatId);
                return Ok(sheduleDaySubject);
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
