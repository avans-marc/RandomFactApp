using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Infrastructure.FunGeneratorFactsApi
{
    public class CatFactNinjaApiClient : IRandomFactClient
    {
        private readonly HttpClient _httpClient;

        public CatFactNinjaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RandomFact?> GetRandomFactAsync()
        {
            var response = await _httpClient.GetAsync("fact");

            // If the request is not successfull, throw an exception
            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<CatFactNinjaApiResponse>();

            // Map to our domain model
            return new RandomFact { Text = responseModel!.fact };
        }
    }
}
