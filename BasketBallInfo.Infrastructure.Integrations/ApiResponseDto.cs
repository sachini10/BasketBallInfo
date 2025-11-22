namespace BasketBallInfo.Infrastructure.Integrations;

public record ApiResponseDto
{
    public string Get { get; init; }
    public ParametersDto Parameters { get; init; }
    public List<GamesDto> Response { get; init; }
}

public record ParametersDto
{
    public string Date { get; init; }
}
public record GamesDto
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public StatusDto Status { get; init; }
    public CountryDto Country { get; init; }
    public TeamsDto Teams { get; init; }
}

public record StatusDto
{
    public string Long { get; init; }
}

public record CountryDto
{
    public string Name { get; init; }
}

public record TeamsDto
{
    public TeamDto Home { get; init; }
    public TeamDto Away { get; init; }
}

public record TeamDto
{
    public string Name { get; init; }
}
