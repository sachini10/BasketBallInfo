using BasketBallInfo.Application.GetGames;
using BasketBallInfo.Application.ServiceContracts;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BasketBallInfo.Infrastructure.Integrations
{
    public class BasketBallDetailsService : IBasketBallDetailsService
    {
        private readonly HttpClient _httpClient;
        private readonly SportsApiConfigs _apiConfigs;

        public BasketBallDetailsService(HttpClient httpClient, IOptions<SportsApiConfigs> apiConfigs)
        {
            _httpClient = httpClient;
            _apiConfigs = apiConfigs.Value;
        }
        public async Task<List<GameDetailsDto>> FetchGameDetailsAsync(CancellationToken ct = default)
        {
            var gameDetailsList = new List<GameDetailsDto>();
            var request = new HttpRequestMessage(HttpMethod.Get, _apiConfigs.BaseUrl);
            request.Headers.Add("x-rapidapi-host", _apiConfigs.Host);
            request.Headers.Add("x-rapidapi-key", _apiConfigs.Key);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiResponse = JsonSerializer.Deserialize<ApiResponseDto>(json, options);
            if (apiResponse?.Response is null) return gameDetailsList;

            foreach (var item in apiResponse.Response)
            {
                var gameDetails = new GameDetailsDto(
                    item.Id,
                    item.Date,
                    item.Status.Long,
                    item.Country.Name,
                    item.Teams.Home.Name,
                    item.Teams.Away.Name
                );
                gameDetailsList.Add(gameDetails);
            }
            return gameDetailsList;
        }


    }

}
