using Microsoft.AspNetCore.Mvc.Filters;
using NuevoInterView.CMS.Controllers;

namespace NuevoInterView.CMS.Helper
{
    public class LoginFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = (string)context.RouteData.Values["action"];
            string controllerName = (string)context.RouteData.Values["controller"];
            var controller = (HomeController)context.Controller;
            context.HttpContext.Session.TryGetValue("token", out var result);

            if (result == null)
            {
                context.Result = controller.RedirectToAction("Login", "Account");
            }


        }


    }
}
