using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace zBus.Filters
{
    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public RoleAuthorizationFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");
            if (_role != role)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
                return;
            }
        }
    }

    public class LoginAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userEmail = context.HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                context.Result = new RedirectToActionResult("LoginReminder", "Home", null);
            }
        }
    }
}
