namespace BasketBallInfo.Application.GetGames;

public interface IGameDetailsService
{
    public Task<List<GameDetailsResponse>> GetGameDetails();
}