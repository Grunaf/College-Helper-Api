using app.Interfaces;
using app.Interfaces.StudentAbsense;
using app.Mappers;
using app.Repository;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/studentgroups")]
    [ApiController]
    public class StudentGroupController(IStudentGroupRepository studentGroupRepo, IStudentAbsenceService studentAbsenceService, ILogger<StudentGroupController> logger) : ControllerBase
    {
        private readonly IStudentGroupRepository _studentGroupRepo = studentGroupRepo;
        private readonly IStudentAbsenceService _studentAbsenceService = studentAbsenceService;
        private readonly ILogger<StudentGroupController> _logger = logger;

/*        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var studentGroupModels = await _studentGroupRepo.GetAllAsync();
            var studentGroupDto = studentGroupModels.Select(sg => sg.ToStudentGroupDto());

            return Ok(studentGroupDto);
        }*/
    }
}
