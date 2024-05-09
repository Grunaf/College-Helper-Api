using app.Interfaces;
using app.Mappers;
using app.Repository;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/studentgroups")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IStudentGroupRepository _studentGroupRepo;
        public StudentGroupController(ApplicationContext context, IStudentGroupRepository studentGroupRepo)
        {
            _context = context;
            _studentGroupRepo = studentGroupRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var studentGroupModels = await _studentGroupRepo.GetAllAsync();
            var studentGroupDto = studentGroupModels.Select(sg => sg.ToStudentGroupDto());

            return Ok(studentGroupDto);
        }
    }
}
