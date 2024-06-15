using app.Dtos.StudentAbsence;
using app.Interfaces.StudentAbsense;
using Microsoft.AspNetCore.Mvc;

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
    }
}
