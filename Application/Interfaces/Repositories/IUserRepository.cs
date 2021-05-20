using Quotes.Domain.Entities;
using System.Threading.Tasks;

namespace Quotes.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
    }
}
