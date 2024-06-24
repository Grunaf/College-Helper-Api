using app.Dtos;
using app.Dtos.SheduleDay;
using app.Dtos.Student;
using app.Dtos.StudentAbsence;
using app.Exceptions;
using app.Interfaces;
using app.Interfaces.Shedule;
using app.Interfaces.StudentAbsense;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/studentgroups/by-head-boy-chat-id/")]
    [ApiController]
    public class StudentGroupController(IStudentAbsenceService studentAbsenceService,
        IStudentService studentService, IHomeworkService homeworkService, IStudentGroupService studentGroupService,
        ISheduleService sheduleService, ILogger<StudentGroupController> logger) : ControllerBase
    {
        private readonly IStudentGroupService _studentGroupService = studentGroupService;
        private readonly IStudentAbsenceService _studentAbsenceService = studentAbsenceService;
        private readonly IStudentService _studentService = studentService;
        private readonly ISheduleService _sheduleService = sheduleService;
        private readonly IHomeworkService _homeworkService = homeworkService;
        private readonly ILogger<StudentGroupController> _logger = logger;


        [HttpGet("{headBoyChatId}/absences/{date}/{lessonNumber}")]
        public async Task<IActionResult> GetListAttendanceOnPair(long headBoyChatId, DateTime date, byte lessonNumber)
        {
            try
            {
                var studentAttendanceDtos = await _studentAbsenceService.GetListAttendanceOnPairAsync(headBoyChatId, date, lessonNumber);
                if (studentAttendanceDtos == null)
                {
                    return NotFound();
                }
                return Ok(studentAttendanceDtos);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message  );
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
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

        [HttpGet("{headBoyChatId}/absences/previous-semester")]
        public async Task<IActionResult> GetAbsencesCountForSemesterByHeadboyChatId(long headBoyChatId)
        {
            try
            {
                var statStudentAbsense = await _studentAbsenceService.GetAbsencesCountForPeriodByHeadboyChatId(headBoyChatId);
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
                return StatusCode(403, ex.Message);
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

        [HttpPost("{headBoyChatId}/absences/today/")]
        public async Task<IActionResult> HandleStudentAbsence(long headBoyChatId, CreateOrDeleteStudentAbsenceRequestDto absenceDto)
        {
            try
            {
                var studentAbsenceModel = await _studentAbsenceService.CreateOrDeleteAsync(headBoyChatId, absenceDto);

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
                return StatusCode(403, ex.Message);
            }
            catch (StudentNotInGroupException ex)
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

        [HttpPost("{headBoyChatId}/homeworks")]
        public async Task<IActionResult> AddByHeadBoyChatId(long headBoyChatId, CreateHomeworkRequestDto createHomeworkRequestDto)
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
                return StatusCode(403, ex.Message);
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

        [HttpPost("{headBoyChatId}/homeworks/{homeworkId}")]
        public async Task<IActionResult> UpdateHomework(long headBoyChatId, int homeworkId, CreateHomeworkRequestDto createHomeworkRequestDto)
        {
            try
            {
                var homework = await _homeworkService.UpdateByHeadBoyChatIdAsync(headBoyChatId, homeworkId, createHomeworkRequestDto);
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
                return StatusCode(403, ex.Message);
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



        [HttpPost("{headBoyChatId}/shedule")]
        public async Task<IActionResult> CreateShedule(long headBoyChatId, SheduleRequestDto sheduleDto)
        {
            try
            {
                var createdSheduleDto = await _sheduleService.CreateSheduleAsync(headBoyChatId, sheduleDto);
                return Ok(createdSheduleDto);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера: {ErrorMessage}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{headBoyChatId}/hasOtherStudent")]
        public async Task<IActionResult> HasAnyoneInSomeGroupExceptHeadBoyByHeadBoyChatId(long headBoyChatId)
        {
            try
            {
                var headBoy = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);
                await _studentGroupService.CheckIsStudentsInGroupOrThrowExceptionAsync(headBoy.StudentGroupId);
                return Ok(true);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
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


        [HttpPost("{headBoyChatId}/students")]
        public async Task<IActionResult> AddStudentsInGroup(long headBoyChatId, List<StudentDto> studentsDto)
        {
            try
            {
                var createdStudentsDto = await _studentGroupService.AddStudentsToGroupAsync(headBoyChatId, studentsDto);
                return Ok(createdStudentsDto);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
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

        [HttpGet("{headBoyChatId}/students")]
        public async Task<IActionResult> GetStudentFromGroupExceptHeadBoy(long headBoyChatId)
        {
            try
            {
                var students = await _studentGroupService.GetAllStudentsExceptHeadBoyInGroupFromHeadBoy(headBoyChatId);
                return Ok(students);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
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

        [HttpPut("{headBoyChatId}/students/{studentId}")]
        public async Task<IActionResult> UpdateStudentInGroup(long headBoyChatId, int studentId, StudentDto studentDto)
        {
            try
            {
                var updatedStudentsDto = await _studentGroupService.UpdateStudentInGroupAsync(headBoyChatId, studentId, studentDto);
                return Ok(updatedStudentsDto);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (StudentNotInGroupException ex)
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

        [HttpDelete("{headBoyChatId}/students/{studentChatId}")]
        public async Task<IActionResult> DeleteStudentInGroup(long headBoyChatId, int studentChatId)
        {
            try
            {
                var deletedStudentsDto = await _studentGroupService.DeleteStudentInGroupAsync(headBoyChatId, studentChatId);
                return NoContent();
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (NotHeadBoyException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return StatusCode(403, ex.Message);
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении операции: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (StudentNotInGroupException ex)
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

                [HttpGet("{headBoyChatId}/hasOtherStudent")]
                public async Task<IActionResult> HasAnyoneInSomeGroupExceptHeadBoyByHeadBoyChatId(long headBoyChatId)
                {
                    try
                    {
                        var hasAnyone = await _studentService.HasAnyoneInSomeGroupExceptHeadBoyByHeadBoyChatId(headBoyChatId);
                        return Ok(hasAnyone);
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
        /*        [HttpGet]
                public async Task<IActionResult> GetAllAsync()
                {
                    var studentGroupModels = await _studentGroupRepo.GetAllAsync();
                    var studentGroupDto = studentGroupModels.Select(sg => sg.ToStudentGroupDto());

                    return Ok(studentGroupDto);
                }*/
    }

}
