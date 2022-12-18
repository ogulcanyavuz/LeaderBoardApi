using LeaderBoardApi.Entities;
using LeaderBoardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : Controller
    {

        private readonly PrizeService _prizeService;

        public PrizeController(PrizeService prizeService) =>
            _prizeService = prizeService;

        [HttpPost]
        [Route("CreatePrize")]
        public async Task<ActionResult> CreatePost()
        {
            var prizeTask = _prizeService.CreatePrizeAsync();
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
        [Route("GetPrizeAll")]
        public async Task<List<Prize>> Get() =>
               await _prizeService.GetAllAsync();

    }
}
