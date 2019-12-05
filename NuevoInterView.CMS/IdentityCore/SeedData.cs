using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NuevoInterView.CMS.IdentityCore
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<nuevoDBContext>())
            {
                context.Database.Migrate();
            }
         

         
        }
    }
}
