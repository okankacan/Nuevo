using Microsoft.EntityFrameworkCore;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IBaseRepository<TModel, CustomDbContext> : IBaseGenericRepository<TModel, int, CustomDbContext>
            where TModel : class, IDbEntity<int>
            where CustomDbContext : DbContext
    {

    }
}
