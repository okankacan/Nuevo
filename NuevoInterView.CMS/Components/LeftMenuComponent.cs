using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuevoInterView.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Components
{
    public class LeftMenuComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LeftMenuComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var roles = (await _userManager.GetRolesAsync(user)).OrderBy(c => c);
            return View();
        }

    }

}
