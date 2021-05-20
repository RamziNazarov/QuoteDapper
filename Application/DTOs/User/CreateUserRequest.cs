using System;

namespace Quotes.Application.DTOs.User
{
    public class CreateUserRequest
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        public string SecretWord { get; set; }
    }
}
