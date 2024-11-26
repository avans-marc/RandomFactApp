using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Clients;
using System.Net.Http.Json;

namespace RandomFactApp.Infrastructure.UselessFactsJsphPIApi
{
    /// <summary>
    /// Implements the RandomFactClient interface to fetch
    /// random facts over HTTP
    /// </summary>
    public class UselessFactsJsphPlApiClient : IRandomFactClient
    {
        private readonly HttpClient _httpClient;

        public UselessFactsJsphPlApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RandomFact?> GetRandomFactAsync()
        {
            var response = await _httpClient.GetAsync("facts/random");

            // If the request is not successfull, throw an exception
            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<UselessFactsJsphPlApiResponse>();

            // Map to our domain model
            return new RandomFact 
            { 
                Text = responseModel!.text, 
                Location = GpsLocation.CreateRandomLocation() 
            };
        }

     
    }
}
