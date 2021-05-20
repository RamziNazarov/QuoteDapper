using Quotes.Application.DTOs.User;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Application.Interfaces.Services;
using Quotes.Domain.Wrappers;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> AuthenticateAsync(AuthenticateUserRequest request)
        {
            var user = await _userRepository.GetById(request.Id);

            if (user == null)
                return new Response { Succeeded = false, Message = "Пользователь не найден!" };

            var newMessage = GenerateHMac(user.UserId, user.SecretWord);

            if (newMessage != request.Message)
                return new Response { Succeeded = false, Message = "Неправильный MessageDigest" };

            return new Response { Succeeded = true, Message = default };
        }

        private static byte[] ConvertFromBase64String(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return null;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GenerateHMac(string key, string message)
        {
            var decodedKey = ConvertFromBase64String(key);

            var hasher = new HMACSHA256(decodedKey);

            var messageBytes = Encoding.Default.GetBytes(message);

            var hash = hasher.ComputeHash(messageBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
