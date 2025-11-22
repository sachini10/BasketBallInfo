using BasketBallInfo.Application.GetGames;
using BasketBallInfo.Application.ServiceContracts;
using System.Text.Json;

namespace BasketBallInfo.Infrastructure.Integrations
{
    public class BasketBallDetailsService : IBasketBallDetailsService
    {
        private readonly HttpClient _httpClient;

        public BasketBallDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<GameDetailsResponse>> FetchGameDetailsAsync(CancellationToken ct = default)
        {
            var gameDetailsList = new List<GameDetailsResponse>();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://v1.basketball.api-sports.io/games?date=2025-11-21");
            request.Headers.Add("x-rapidapi-host", "v1.basketball.api-sports.io");
            request.Headers.Add("x-rapidapi-key", "4c93ac4d8cca3b8a4c023bc02d79e08e");
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
                var gameDetails = new GameDetailsResponse(
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
