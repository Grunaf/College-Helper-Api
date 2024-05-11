using app.Dtos.User;
using app.Interfaces;
using app.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepo;
        public UserController(ApplicationContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var usersDto = users.Select(u => u.ToUserDto());
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userModel = await _userRepo.GetByIdAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromCreateDto();
            await _userRepo.CreateAsync(userModel);
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = await _userRepo.UpdateAsync(id, userDto);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel.ToUserDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
            {
                var userModel = await _userRepo.DeleteAsync(id);
                if (userModel == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
}
