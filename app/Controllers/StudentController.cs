using app.Interfaces;
using app.Mappers;
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
        private readonly IStudentRepository _userRepo;
        public StudentController(ApplicationContext context, IStudentRepository userRepo) 
        {
            _context = context;
            _userRepo = userRepo;
        }

        [HttpGet("{headBoyId}")]
        public async Task<IActionResult> GetStudentsByIdHeadBoyId(long headBoyId)
        {
            var users = await _userRepo.GetStudentsByIdHeadBoyId(headBoyId);
            if (users == null)
            {
                return NotFound();
            }
            var usersDto = users.Select(u => u.ToUserAttendanceDto());
            return Ok(usersDto);
        }
    }
}
