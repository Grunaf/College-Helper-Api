using app.Interfaces;
using app.Mappers;
using app.Services;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace app.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IStudentService _studentService;
        public StudentController(ApplicationContext context, IStudentService studentService) 
        {
            _context = context;
            _studentService = studentService;
        }

        [HttpGet("{headBoyChatId}/{date}/{lessonNumber}")]
        public async Task<IActionResult> GetStudentsByHeadBoyChatId(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            var studentAttendanceDtos = await _studentService.GetStatusStudentFromListAttendanceAsync(headBoyChatId, date, lessonNumber);
            if (studentAttendanceDtos == null)
            {
                return NotFound();
            }
            return Ok(studentAttendanceDtos);
        }
        [HttpGet("{chatId}/role")]
        public async Task<IActionResult> GetRoleByChatId(long chatId)
        {
            try
            {
                var isHeadBoy = await _studentService.IsStudentHeadboyByChatIdAsync(chatId);
                return Ok(isHeadBoy);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}
