using app.Dtos.StudentAttendance;
using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [Route("api/studentattendance")]
    [ApiController]
    public class StudentsAttendanceController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IStudentAttendanceRepository _studentAttendanceRepo;
        public StudentsAttendanceController(ApplicationContext context, IStudentAttendanceRepository studentAttendanceRepo)
        {
            _context = context;
            _studentAttendanceRepo = studentAttendanceRepo;
        }
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
        
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] StudentAttendanceCreateRequestDto studentAttendanceDto)
        {
            var studentAttendanceModel = studentAttendanceCreateRequestDto.ToStudentAttendanceFromCreateDto();
            await _studentAttendanceRepo.CreateAsync(studentAttendanceModel);

            return CreatedAtAction(nameof(GetAllById), new { studentId = studentAttendanceModel.StudentId}, studentAttendanceModel.ToStudentAttendanceByIdDto());
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
