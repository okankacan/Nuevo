using Microsoft.EntityFrameworkCore;
using NuevoInterView.CMS.Repository.Interface;

namespace NuevoInterView.CMS.Repository
{


    public class BaseRepository<TModel, CustomDbContext> : BaseGenericRepository<TModel, int, CustomDbContext>, IBaseRepository<TModel, CustomDbContext>
    where TModel : class, IPersistentEntity
    where CustomDbContext : DbContext
    {
    }
}

 