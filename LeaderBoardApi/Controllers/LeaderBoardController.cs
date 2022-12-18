using LeaderBoardApi.Entities;
using LeaderBoardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly LeaderBoardApiService _leaderBoardApiService;

        public LeaderBoardController(LeaderBoardApiService leaderBoardApiService) =>
            _leaderBoardApiService = leaderBoardApiService;

        [HttpPost]
        [Route("CreateLeaderBoard")]
        public async Task<ActionResult> CreatePost(int month, int year)
        {
            var leaderBoardTask = _leaderBoardApiService.CreateLeaderBoard(month, year);
            if (leaderBoardTask.IsFaulted)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("GetLeaderBoardByDate")]
        public async Task<ActionResult<LeaderBoard>> GetBoard(int gMonth, int gYear)
        { 
            var leaderBoard = await _leaderBoardApiService.GetAsync(gMonth, gYear);
            if (leaderBoard is null)
            {
                return NotFound();
            }

            return leaderBoard;
        }

        [HttpGet]
        [Route("GetLeaderBoardByUser")]
        public async Task<ActionResult<User>> GetBoard(int gMonth, int gYear,string userId)
        {
            var leaderBoard = await _leaderBoardApiService.GetLeaderBoardUserAsync(gMonth, gYear,userId);
            if (leaderBoard is null)
            {
                return NotFound();
            }

            return leaderBoard;
        }
    }
}