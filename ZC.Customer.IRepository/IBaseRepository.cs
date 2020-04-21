using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;

namespace ZC.Customer.IRepository
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class, new()
    {
        DbContext DbContext { get; }

        #region 查询

        TEntity First();

        Task<TEntity> FirstAsync();

        TEntity First(Expression<Func<TEntity, bool>> where);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> where);

        TEntity FirstOrDefault();

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Last();

        Task<TEntity> LastAsync();

        TEntity Last(Expression<Func<TEntity, bool>> where);

        Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> where);

        TEntity LastOrDefault();

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> LastOrDefaultAsync();

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(TKey id);

        Task<TEntity> GetByIdAsync(TKey id);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        List<TEntity> GetAllList();

        Task<List<TEntity>> GetAllListAsync();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<PagedResult<TEntity>> QueryPageAsync<S>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, S>> orderBy, int pageSize, int pageIndex, bool asc = true);

        Task<PagedResult<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> @where, string orderBy, int pageSize, int pageIndex);

        #endregion

        #region 新增

        bool Insert(TEntity entity);

        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity);

        void InsertRange(List<TEntity> entities);

        Task InsertRangeAsync(List<TEntity> entities);

        #endregion

        #region 修改

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion

        #region 删除

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        int SaveChanged();
    }
}
