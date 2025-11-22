namespace BasketBallInfo.Application.GetGames;

public record GameDetailsDto(int GameId, DateTime Date, string Status, string CountryName, string Team1Name, string Team2Name);

