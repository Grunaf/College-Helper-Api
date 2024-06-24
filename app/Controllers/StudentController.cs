using app.Exceptions;
using app.Interfaces;
using app.Interfaces.Shedule;
using app.Mappers;
using app.Services;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace app.Controllers
{
    [Route("api/students/by-chat-id")]
    [ApiController]
    public class StudentController(ApplicationContext context, IStudentService studentService, IHomeworkService homeworkService,
        ISheduleService sheduleService, ISubjectService subjectService, ILogger<StudentController> logger) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IHomeworkService _homeworkService = homeworkService;
        private readonly ISheduleService _sheduleService = sheduleService;
        private readonly ISubjectService _subjectService = subjectService;
        private readonly ILogger<StudentController> _logger = logger;

        [HttpGet("{studentChatId}/homeworks/subjects/{subjectId}")]
        public async Task<IActionResult> GetSubjectHomeworks(long studentChatId, int subjectId)
        {
            try
            {
                var homeworks = await _homeworkService.GetBySubjectIdAndStudentChatIdAsync(studentChatId, subjectId);
                if (homeworks.Count == 0) throw new DataNotFoundException("Не найдены домашние задания по предмету");

                return Ok(homeworks);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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
        [HttpGet("{studentChatId}/homeworks/{homeworkId}")]
        public async Task<IActionResult> GetHomework(long studentChatId, int homeworkId)
        {
            try
            {
                var homework = await _homeworkService.GetByIdByStudentChatIdAsync(studentChatId, homeworkId);
                return Ok(homework);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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

        [HttpGet("{studentChatId}/next-day-schedule/")]
        public async Task<IActionResult> GetNextDayShedule(long studentChatId)
        {
            try
            {
                var sheduleDaySubject = await _sheduleService.GetTommorowSheduleDayByStudentChatIdAsync(studentChatId);
                return Ok(sheduleDaySubject);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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

        [HttpGet("{studentChatId}/schedule/")]
        public async Task<IActionResult> GetSheduleFor(long studentChatId)
        {
            try
            {
                var sheduleDto = await _sheduleService.GetSheduleByChatIdAsync(studentChatId);
                return Ok(sheduleDto);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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


        [HttpGet("{studentChatId}/subjects/actual/with-homework")]
        public async Task<IActionResult> GetSubjectsWithHomework(long studentChatId)
        {
            try
            {
                var subjectDtoWithHomeworks = await _subjectService.GetSubjectsWithHomeworkByStudentChatIdAsync(studentChatId);
                return Ok(subjectDtoWithHomeworks);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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

        [HttpGet("{studentChatId}/subjects/actual")]
        public async Task<IActionResult> GetActualSubjects(long studentChatId)
        {
            try
            {
                var subjectDto = await _subjectService.GetActualSubjectsByStudentChatIdAsync(studentChatId);
                return Ok(subjectDto);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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

        [HttpGet("{studentChatId}/finished-subjects")]
        public async Task<IActionResult> GetFinishedSubjectsByStudent(long studentChatId)
        {
            throw new NotImplementedException();
        }


        [HttpGet("{chatId}/role")]
        public async Task<IActionResult> GetInfoByChatId(long chatId)
        {
            try
            {
                var studentInfo = await _studentService.GetInfoByChatIdAsync(chatId);
                return Ok(studentInfo);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
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
