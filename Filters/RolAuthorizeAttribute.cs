using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ing_Soft.Filters
{
    public class RolAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _rol;

        public RolAuthorizeAttribute(string rol)
        {
            _rol = rol;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rolUsuario = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rolUsuario) || rolUsuario != _rol)
            {
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
            }
        }
    }
}
