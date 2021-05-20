using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Quotes.Application.DTOs.User;
using Quotes.Application.Interfaces.Services;
using Quotes.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Persistence.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private const string UserId = "X-UserId";
        private const string MessageDigest = "X-MessageDigest";
        private readonly IServiceProvider _serviceProvider;

        public AuthenticateAttribute(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                var id = request.Headers[UserId].ToString();
                var message = request.Headers[MessageDigest].ToString();

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(message))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                Response response;
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    response = userService.AuthenticateAsync(new AuthenticateUserRequest { Id = id, Message = message }).Result;
                }
                if (response.Succeeded)
                {
                    base.OnActionExecuting(context);
                    return;
                }
                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = 400;
                return;
            }
            catch
            {
                return;
            }
        }
    }
}
