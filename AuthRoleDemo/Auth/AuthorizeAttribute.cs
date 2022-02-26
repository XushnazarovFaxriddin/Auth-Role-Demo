using AuthRoleDemo.Entites;
using AuthRoleDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthRoleDemo.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                // agar harakat [AllowAnonymous] atributi bilan bezatilgan bo'lsa, avtorizatsiyani o'tkazib yuboring
                var allowAnonimus = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonimus)
                    return;


                //ruxsat
                var user = (User)context.HttpContext.Items["User"];
                if (user is null || (_roles.Any() && !_roles.Contains(user.Role)))
                {
                    //tizimga kirmagan yoki rolga ruxsat berilmagan
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(new { error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
