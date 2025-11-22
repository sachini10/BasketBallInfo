using BasketBallInfo.Application.ServiceContracts;
using BasketBallInfo.Context;
using Microsoft.Data.SqlClient;

namespace BasketBallInfo.Application.GetGames;

public class GameDetailsService : IGameDetailsService
{
    private readonly SqlConnectionFactory _factory;
    private readonly IBasketBallDetailsService _basketBallDetailsService;

    public GameDetailsService(SqlConnectionFactory factory,
        IBasketBallDetailsService basketBallDetailsService)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _basketBallDetailsService = basketBallDetailsService ?? throw new ArgumentNullException(nameof(basketBallDetailsService));
    }

    public async Task<List<GameDetailsDto>> GetGameDetails()
    {
        const string sql = @"
            SELECT GameId, Date, Status, CountryName, Team1Name, Team2Name
            FROM Games
            WHERE CAST(Date AS DATE) = @Date;
        ";
        var games = new List<GameDetailsDto>();
        using var connection = _factory.Create();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@Date", "2025-11-21");
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            games.Add(new GameDetailsDto(
                reader.GetInt32(0),
                reader.GetDateTime(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            ));
        }
        connection.Close();
        if (games.Count > 0) return games;

        games = await _basketBallDetailsService.FetchGameDetailsAsync();
        if (games.Count == 0) return games;
        await AddGameDetails(games, connection);
        return games;
    }

    private static async Task AddGameDetails(List<GameDetailsDto> games, SqlConnection connection)
    {
        connection.Open();
        foreach (var game in games)
        {
            using var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO Games (GameId, Date, Status, CountryName, Team1Name, Team2Name)
                VALUES (@GameId, @Date, @Status, @CountryName, @Team1Name, @Team2Name);
            ";
            insertCommand.Parameters.AddWithValue("@GameId", game.GameId);
            insertCommand.Parameters.AddWithValue("@Date", game.Date);
            insertCommand.Parameters.AddWithValue("@Status", game.Status);
            insertCommand.Parameters.AddWithValue("@CountryName", game.CountryName);
            insertCommand.Parameters.AddWithValue("@Team1Name", game.Team1Name);
            insertCommand.Parameters.AddWithValue("@Team2Name", game.Team2Name);

            await insertCommand.ExecuteNonQueryAsync();
        }
        connection.Close();
    }

    public async Task<GameDetailsDto> GetGameDetailsById(int id, CancellationToken ct)
    {
        const string sql = @"
            SELECT GameId, Date, Status, CountryName, Team1Name, Team2Name
            FROM Games
            WHERE gameId = @GameId;
        ";
        using var connection = _factory.Create();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@GameId", id);
        await connection.OpenAsync(ct);
        using var reader = await command.ExecuteReaderAsync(ct);

        if (!reader.Read()) return null;

        return new GameDetailsDto(
            reader.GetInt32(0),
            reader.GetDateTime(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5)
        );
    }
}
