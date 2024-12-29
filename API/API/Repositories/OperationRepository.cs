using API.Model;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly List<Model.Operation> _permissions = new List<Model.Operation>
        {
            new() { Id = 1, Name = "GET" },
            new() { Id = 2, Name = "POST" },
            new() { Id = 3, Name = "PUT" },
            new() { Id = 4, Name = "DELETE" }
        };

        public Model.Operation? Get(string name)
        {
            return _permissions.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}