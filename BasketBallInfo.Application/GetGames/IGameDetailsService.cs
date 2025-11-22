namespace BasketBallInfo.Application.GetGames;

public interface IGameDetailsService
{
    Task<List<GameDetailsDto>> GetGameDetails();
    Task<GameDetailsDto> GetGameDetailsById(int id, CancellationToken ct);
}