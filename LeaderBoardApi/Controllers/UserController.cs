using LeaderBoardApi.Entities;
using LeaderBoardApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpPost]
        [Route("CreateUserData")]
        public async Task<ActionResult> CreateUserData()
        {
            var prizeTask = _userService.CreateUserAsync();
            if (prizeTask.IsFaulted)
            {
                return NoContent();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("GetUserWithPrize")]
        public async Task<ActionResult<List<User>>> GetBoard()
        {
            var leaderBoard = await _userService.GetUserWithPrize();
            if (leaderBoard is null)
            {
                return NotFound();
            }

            return leaderBoard;
        }
    }
}
