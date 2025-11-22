using BasketBallInfo.Application.GetGames;

namespace BasketBallInfo.Application.ServiceContracts;

public interface IBasketBallDetailsService
{
    Task<List<GameDetailsResponse>> FetchGameDetailsAsync(CancellationToken ct = default);
}
