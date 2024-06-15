using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepository, IStudentGroupRepository studentGroupRepository) : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepo = subjectRepository;
        private readonly IStudentGroupRepository _groupRepo = studentGroupRepository;

        [HttpGet("{headboyChatId}")]
        public async Task<IActionResult> GetAllByHeadboyChatId(long headboyChatId)
        {
            try
            {
                var groupModel = await _groupRepo.GetByHeadBoyChatIdAsync(headboyChatId);
                var subjectModels = await _subjectRepo.GetAllActualByStudentGroupIdAsync(groupModel.Id);
                return Ok(subjectModels.Select(s => s.ToGetSubjectRequestDto()));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
