﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Application.DTOs.User
{
    public class AuthenticateUserRequest
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}
