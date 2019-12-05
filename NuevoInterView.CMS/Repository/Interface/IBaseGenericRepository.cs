using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IBaseGenericRepository<TModel, TKey, CustomDbContext>
       where TModel : class, IDbEntity<TKey>
       where CustomDbContext : DbContext
    {
        CustomDbContext Context { get; }
 
        DbSet<TModel> DbSet { get; }

        #region async methods

        Task<TModel> FindAsync(TKey id);

        

        Task<(bool success, int count)> SaveOrUpdateAsync(TModel entity);
      
        Task<(bool success, int count)> SaveWithoutCatchAsync(TModel entity);

  
 
        Task<List<TModel>> GetListAsync();
         

   
        #endregion

         
    }
}
