using BasketBallInfo.Context;

namespace BasketBallInfo.Application.GetGames;

public class GameDetailsService : IGameDetailsService
{
    private readonly SqlConnectionFactory _factory;

    public GameDetailsService(SqlConnectionFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<List<GameDetailsResponse>> GetGameDetails()
    {

        const string sql = @"
            SELECT GameId, Date, Status, CountryName, Team1Name, Team2Name
            FROM Games
            WHERE date = @Date;
        ";
        var games = new List<GameDetailsResponse>();
        using var connection = _factory.Create();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@Date", "2025-11-21");

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            games.Add(new GameDetailsResponse(
                reader.GetInt32(0),
                reader.GetDateTime(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            ));
        }
        return games;
    }

}
