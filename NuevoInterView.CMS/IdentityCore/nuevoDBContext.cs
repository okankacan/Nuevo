using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuevoInterView.CMS.Models;

namespace NuevoInterView.CMS.IdentityCore
{
    public class nuevoDBContext : IdentityDbContext<ApplicationUser>
    {
        public nuevoDBContext( DbContextOptions<nuevoDBContext> options)
            :base (options)
        {

        }

    }
}
