using app.Dtos.StudentAbsence;
using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [Route("api/studentattendance")]
    [ApiController]
    public class StudentAbsenceController : ControllerBase
    {
        private readonly IStudentAbsenceService _studentAbsenceService;
        public StudentAbsenceController(IStudentAbsenceService studentAbsenceService)
        {
            _studentAbsenceService = studentAbsenceService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] CreateOrDeleteStudentAbsenceRequestDto studentAbsenceDto)
        {
            var studentAbsenceResult = await _studentAbsenceService.CreateOrDeleteAsync(studentAbsenceDto);
            if (studentAbsenceResult.StudentAbsence == null)
            {
                return NotFound();
            }
            else if (studentAbsenceResult.OperationType == Services.OperationType.Delete)
            {
                return NoContent();
            }
            return Ok(studentAbsenceResult.StudentAbsence.Id);
        }
        [HttpGet("{headBoyChatId}")]
        public async Task<IActionResult> GetStatAbsenseForStudentsInGroup(long headBoyChatId)
        {
            var statStudentAbsense = await _studentAbsenceService.GetStatStudentAbsensesRequestDtosAsync(headBoyChatId);
            if (statStudentAbsense == null)
            {
                return NotFound();
            }
            return Ok(statStudentAbsense);
        }
        /*        [HttpGet("{headBoyChatId}")]
                public async Task<IActionResult> GetStudentAttendanceGroupByNumLesson(long headBoyChatId)
                {
                    var attendances = await _studentAttendanceRepo.GetAllByHeadBoyChatIdAsync(headBoyChatId);

                    if (attendances == null)
                    {
                        return NotFound();
                    }
                    var attendancesDto = attendances.Select(sa => sa.ToStudentAttendanceByIdDto());
                    return Ok(attendancesDto);
                }*/
        /*
                [HttpGet("{studentId}")]
                public async Task<IActionResult> GetAllById(long studentId)
                {
                    var attendances = await _studentAttendanceRepo.GetAllByStudentIdAsync(studentId);
                    if (attendances == null)
                    {
                        return NotFound();
                    }
                    var attendancesDto = attendances.Select(sa => sa.ToStudentAttendanceByIdDto());
                    return Ok(attendancesDto);
                }



                [HttpPut("{studentId}")]
                public async Task<IActionResult> Update(long studentId, [FromQuery] StudentAttendanceUpdateRequestDto studentAttendanceDto)
                {
                    var studentAttendanceModel = studentAttendanceCreateRequestDto.ToStudentAttendanceFromCreateDto();
                    await _studentAttendanceRepo.CreateAsync(studentAttendanceModel);

                    return CreatedAtAction(nameof(GetAllById), new { studentId = studentAttendanceModel.StudentId}, studentAttendanceModel.To());
                }*/

    }
}
