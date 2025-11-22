using BasketBallInfo.Application.GetGames;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasketBallInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameDetailsService _gameDetails;

        public GamesController(IGameDetailsService gameDetails)
        {
            _gameDetails = gameDetails ?? throw new ArgumentNullException(nameof(gameDetails));
        }
        // GET: api/<GamesController>
        [HttpGet]
        public async Task<IEnumerable<GameDetailsResponse>> Get()
        {
           var gameDetails = await _gameDetails.GetGameDetails();
            return gameDetails;
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
