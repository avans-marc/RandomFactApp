using RandomFactApp.Domain.Models;

namespace RandomFactApp.Domain.Services
{
    /// <summary>
    /// Implementations of this interface provide functionality
    /// to retrieve random facts from an external datasource
    /// </summary>
    public interface IRandomFactClient
    {
        Task<RandomFact?> GetRandomFactAsync();
    }
}