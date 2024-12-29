using API.Model;

namespace API.Repositories.Interfaces
{
    public interface IOperationRepository
    {
        Model.Operation? Get(string name);
    }
}