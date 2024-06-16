using app.Interfaces;
using app.Mappers;
using app.Services;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace app.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController(ApplicationContext context, IStudentService studentService, ILogger<StudentController> logger) : ControllerBase
    {
        private readonly ApplicationContext _context = context;
        private readonly IStudentService _studentService = studentService;
        private readonly ILogger<StudentController> _logger = logger;

        [HttpGet("{chatId}/role")]
        public async Task<IActionResult> GetRoleByChatId(long chatId)
        {
            try
            {
                var isHeadBoy = await _studentService.IsStudentHeadBoyByChatIdAsync(chatId);
                return Ok(isHeadBoy);
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
    }
}
