using app.Dtos.StudentAbsence;
using app.Exceptions;
using app.Interfaces;
using app.Interfaces.StudentAbsense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [Route("api/studentabsences")]
    [ApiController]
    public class StudentAbsenceController(IStudentAbsenceService studentAbsenceService, IStudentService studentService, ILogger<StudentAbsenceController> logger) : ControllerBase
    {
        private readonly IStudentAbsenceService _studentAbsenceService = studentAbsenceService;
        private readonly IStudentService _studentService = studentService;
        private readonly ILogger<StudentAbsenceController> _logger = logger;
/*
        [HttpPost]
        public async Task<IActionResult> HandleStudentAbsenceRequest(CreateOrDeleteStudentAbsenceRequestDto studentAbsenceDto)
        {
            try
            {
                var studentAbsenceModel = await _studentAbsenceService.CreateOrDeleteAsync(studentAbsenceDto);

                if (studentAbsenceModel == null)
                {
                    return NoContent();
                }
                return Ok(studentAbsenceModel);
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

        [HttpGet("groups/{headBoyChatId}/{date}/{lessonNumber}")]
        public async Task<IActionResult> GetStudentsByHeadBoyChatId(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            try
            {
                var studentAttendanceDtos = await _studentService.GetStatusStudentFromListAttendanceAsync(headBoyChatId, date, lessonNumber);
                if (studentAttendanceDtos == null)
                {
                    return NotFound();
                }
                return Ok(studentAttendanceDtos);
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

        [HttpGet("groups/{headBoyChatId}")]
        public async Task<IActionResult> GetStatAbsenseForStudentsInGroup(long headBoyChatId)
        {
            try
            {
                var statStudentAbsense = await _studentAbsenceService.GetStatStudentAbsensesByHeadBoyChatIdAsync(headBoyChatId);
                if (statStudentAbsense == null) return NoContent();

                return Ok(statStudentAbsense);
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
