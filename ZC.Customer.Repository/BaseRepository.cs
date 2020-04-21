using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZC.Customer.IRepository;
using UMa.Merak.Infrastructure.Dto;
using UMa.Merak.Infrastructure.Helper;

namespace ZC.Customer.Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, new()
    {
        private DbContext _context;

        protected BaseRepository(DbContext context)
        {
            this._context = context;
        }

        public DbContext DbContext => _context;

        public DbSet<TEntity> Table
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #region 查询

        public TEntity First()
        {
            return Table.First();
        }

        public Task<TEntity> FirstAsync()
        {
            return Table.FirstAsync();
        }

        public TEntity First(Expression<Func<TEntity, bool>> where)
        {
            if (null == where)
                return null;
            return Table.First(where);
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> where)
        {
            if (null == where)
                return null;
            return Table.FirstAsync(where);
        }

        public TEntity FirstOrDefault()
        {
            return Query().FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Query().FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().FirstOrDefaultAsync(predicate);
        }

        public TEntity Last()
        {
            return Table.Last();
        }

        public Task<TEntity> LastAsync()
        {
            return Table.LastAsync();
        }

        public TEntity Last(Expression<Func<TEntity, bool>> where)
        {
            if (null == where)
                return null;
            return Table.Last(where);
        }

        public Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> where)
        {
            if (null == where)
                return null;
            return Table.LastAsync(where);
        }

        public TEntity LastOrDefault()
        {
            return Query().LastOrDefault();
        }

        public virtual Task<TEntity> LastOrDefaultAsync()
        {
            return Query().LastOrDefaultAsync();
        }

        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().LastOrDefault(predicate);
        }

        public virtual Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().LastOrDefaultAsync(predicate);
        }

        public TEntity GetById(TKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public IQueryable<TEntity> Query()
        {
            return Table.AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            if (null != where)
                return Table.Where(where);
            else
                return Query();
        }

        public virtual List<TEntity> GetAllList()
        {
            return Query().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Query().ToListAsync();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).ToListAsync();
        }

        public async Task<PagedResult<TEntity>> QueryPageAsync<S>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, S>> orderBy, int pageSize, int pageIndex, bool asc = true)
        {
            IQueryable<TEntity> data = asc ?
               Query().Where(@where).OrderBy(orderBy) :
               Query().Where(@where).OrderByDescending(orderBy);

            var totalCount = await data.CountAsync();
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return new PagedResult<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageCount = pageCount,
                RecordCount = totalCount.ObjToInt(),
                List = await data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }

        public async Task<PagedResult<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> @where, string orderBy, int pageSize, int pageIndex)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "CreationTime";//默认以CreationTime排序
            }

            IQueryable<TEntity> data = Query().OrderByBatch(orderBy);

            if (@where != null)
            {
                data =  data.Where(@where);
            }

            var totalCount = await data.CountAsync();
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return new PagedResult<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageCount = pageCount,
                RecordCount = totalCount.ObjToInt(),
                List = await data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }

        #endregion 查询

        #region 新增

        public bool Insert(TEntity entity)
        {
            if (null == entity)
                return false;
            Table.Add(entity);
            return true;
        }

        public Task<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return Table.AddAsync(entity);
        }

        public void InsertRange(List<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public Task InsertRangeAsync(List<TEntity> entities)
        {
            Table.AddRangeAsync(entities);
            return Task.CompletedTask;
        }

        #endregion 新增

        #region 修改

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        #endregion 修改

        #region 删除

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity item in entities)
                Delete(item);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in Query().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.CompletedTask;
        }

        #endregion 删除

        #region 聚合

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Any(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().AnyAsync(predicate);
        }

        public virtual int Count()
        {
            return Query().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Query().CountAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().CountAsync(predicate);
        }

        public virtual long LongCount()
        {
            return Query().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Query().LongCountAsync();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().LongCountAsync(predicate);
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        #endregion 聚合

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        public int SaveChanged()
        {
            return _context.SaveChanges();
        }
    }
}