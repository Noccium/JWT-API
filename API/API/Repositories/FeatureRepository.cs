using API.Model;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly List<Feature> _features = new List<Feature>()
        {
            new () { Id = 1, Name = "WeatherForecast" }
        };

        public Feature? Get(string name)
        {
            return _features.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
