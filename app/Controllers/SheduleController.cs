using app.Dtos.SheduleDay;
using app.Interfaces.Shedule;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/shedule")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly ISheduleService _sheduleService;
        public SheduleController(ISheduleService sheduleService)
        {
            _sheduleService = sheduleService;
        }
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
                return NotFound(ex.Message);
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
                return NotFound(new { ex.Message });
            }
        }
    }
}
