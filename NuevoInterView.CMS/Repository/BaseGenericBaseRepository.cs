using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NuevoInterView.CMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Repository
{

    public class BaseGenericBaseRepository<TModel, TKey, CustomDbContext> : IBaseGenericRepository<TModel, TKey, CustomDbContext>
   where TModel : class, IDbEntity<TKey>
   where CustomDbContext : DbContext
    {


        public CustomDbContext Context { get; }
       

        public DbSet<TModel> DbSet { get; }

        public BaseGenericBaseRepository(CustomDbContext context, DbSet<TModel> dbSet)
        {
            Context = context;
            DbSet = Context.Set<TModel>();

        }

        protected static readonly Expression<Func<TModel, TKey>> DefaultSortExpression = c => c.Id;

 

        private async Task<List<U>> QueryableToListAsync<U>(IQueryable<U> queryable)
        {
            try
            {
                return await queryable.ToListAsync();
            }
            catch (Exception exc)
            {

                return null;
            }
        }

      
   

        private List<U> QueryableToList<U>(IEnumerable<U> enumerable)
        {
            try
            {
                return enumerable.ToList();
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        

     
        public virtual async Task<TModel> FindAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }


        public virtual async Task<(bool success, int count)> SaveOrUpdateWithoutCatchAsync(TModel entity)
        {
            if (IsPersistent(entity))
            {
                DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity.CreateDate = DateTime.UtcNow;
                DbSet.Add(entity);
            }
        

            var changes = await Context.SaveChangesAsync();

            return (true, changes);
        }



        public virtual async Task<(bool success, int count)> SaveOrUpdateAsync(TModel entity)
        {
            try
            {
                return await SaveOrUpdateWithoutCatchAsync(entity);
            }
            catch (DbUpdateException duEx)
            {
                return (false, 0);
            }
            catch (Exception ex)
            {
                return (false, 0);
            }
        }
         


 
        public virtual async Task<(bool success, int count)> SaveWithoutCatchAsync(TModel entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            DbSet.Add(entity);
          

            var changes = await Context.SaveChangesAsync();

            return (true, changes);
        }

              public virtual async Task<List<TModel>> GetListAsync()
        {
            return await QueryableToListAsync(DbSet.OrderByDescending(DefaultSortExpression).AsNoTracking());
        }

      
      
        
        public virtual async Task<(bool success, bool exists)> ExistsAsync(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return (true, await DbSet.AnyAsync(predicate));
            }
            catch (Exception exc)
            {
                return (false, false);
            }
        }

        
 
  
         
        protected virtual bool IsPersistent(IDbEntity<TKey> entity)
        {
            return !entity.Id.Equals(default(TKey));
        }

      
         
    }


}
