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

        [HttpGet]
        public async Task<List<GameDetailsDto>> Get()
        {
           var gameDetails = await _gameDetails.GetGameDetails();
            return gameDetails;
        }

        [HttpGet("{id}")]
        public async Task<GameDetailsDto> Get(int id, CancellationToken ct)
        {
            var gameDetail = await _gameDetails.GetGameDetailsById(id, ct);
            return gameDetail;
        }
    }
}
