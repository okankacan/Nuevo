using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuevoInterView.CMS.Helper;
using NuevoInterView.CMS.Repository.Interface;

namespace NuevoInterView.CMS.Repository
{


    public class BaseGenericRepository<TModel, TKey, CustomDbContext> : BaseGenericBaseRepository<TModel, TKey, CustomDbContext>
   where TModel : class, IDbEntity<TKey>
   where CustomDbContext : DbContext
    {
       
        public BaseGenericRepository() : base(
        ServiceGetter.GetService<CustomDbContext>()        ,null)
        ///ServiceGetter.GetService<ICacheManager>()
        // ServiceGetter.GetService<ILogger<IBaseGenericRepository<TModel, TKey, CustomDbContext>>>())
        { }
    }
}
