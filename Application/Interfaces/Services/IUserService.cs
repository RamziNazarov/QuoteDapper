using Quotes.Application.DTOs.User;
using Quotes.Domain.Wrappers;
using System.Threading.Tasks;

namespace Quotes.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response> AuthenticateAsync(AuthenticateUserRequest request);
    }
}
