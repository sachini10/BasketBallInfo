using BasketBallInfo.Application.GetGames;

namespace BasketBallInfo.Application.ServiceContracts;

public interface IBasketBallDetailsService
{
    Task<List<GameDetailsDto>> FetchGameDetailsAsync(CancellationToken ct = default);
}
