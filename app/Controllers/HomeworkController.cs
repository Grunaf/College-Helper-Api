using app.Dtos;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/homework")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }
        [HttpPost]
        public async Task<IActionResult> AddByHeadboyChatId([FromQuery] long headboyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            try
            {
                var homework = await _homeworkService.AddByHeadboyChatIdAsync(headboyChatId, createHomeworkRequestDto);
                return Ok(homework);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера. Пожалуйста, попробуйте позже.");
            }
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubjectIdAndHeadboyChatIdAsync(int subjectId, [FromQuery] int headboyChatId)
        {
            try
            {
                var homeworks = await _homeworkService.GetBySubjectIdAndHeadboyChatIdAsync(subjectId, headboyChatId);
                return Ok(homeworks);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера. Пожалуйста, попробуйте позже.");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера. Пожалуйста, попробуйте позже.");
            }
        }
    }
}
