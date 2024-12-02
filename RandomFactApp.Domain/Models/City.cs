using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Domain.Models
{
    public class City
    {
        public City()
        {
            
        }

        public City(double latitude, double longitude, string city)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = city;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }

        public static City CreateRandomLocation()
        {
            Random random = new Random();
                List<(string City, string Country, double Latitude, double Longitude)> worldCities = new List<(string, string, double, double)>
                {
                    ("New York", "USA", 40.7128, -74.0060),
                    ("Tokyo", "Japan", 35.6895, 139.6917),
                    ("London", "UK", 51.5074, -0.1278),
                    ("Paris", "France", 48.8566, 2.3522),
                    ("Sydney", "Australia", -33.8688, 151.2093),
                    ("Mumbai", "India", 19.0760, 72.8777),
                    ("Cape Town", "South Africa", -33.9249, 18.4241),
                    ("Moscow", "Russia", 55.7558, 37.6173),
                    ("Rio de Janeiro", "Brazil", -22.9068, -43.1729),
                    ("Toronto", "Canada", 43.651070, -79.347015),
                    ("Beijing", "China", 39.9042, 116.4074),
                    ("Berlin", "Germany", 52.5200, 13.4050),
                    ("Buenos Aires", "Argentina", -34.6037, -58.3816),
                    ("Cairo", "Egypt", 30.0444, 31.2357),
                    ("Mexico City", "Mexico", 19.4326, -99.1332),
                    ("Seoul", "South Korea", 37.5665, 126.9780),
                    ("Bangkok", "Thailand", 13.7563, 100.5018),
                    ("Lagos", "Nigeria", 6.5244, 3.3792),
                    ("Jakarta", "Indonesia", -6.2088, 106.8456),
                    ("Dubai", "UAE", 25.276987, 55.296249),
                    // Add as many cities as needed to create a good pool for selection
                };


            var position = random.Next(0, worldCities.Count);
            var city = worldCities[position];

            return new City(city.Latitude, city.Longitude, city.City);

        }
    }
}