using API.Model;

namespace API.Repositories.Interfaces
{
    public interface IFeatureRepository
    {
        Feature? Get(string name);
    }
}