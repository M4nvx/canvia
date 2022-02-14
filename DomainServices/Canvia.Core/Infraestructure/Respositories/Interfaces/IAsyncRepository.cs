using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Core.Infraestructure.Respositories.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);

        Task<TEntity> GetById(string id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAllOrder(Func<TEntity, object> order, System.ComponentModel.ListSortDirection sortDirection);

        Task<IEnumerable<TEntity>> Select(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> SelectIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> SelectIncludesWithTake(Expression<Func<TEntity, bool>> predicate, int take, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> SelectIncludesWithPagination(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, System.ComponentModel.ListSortDirection sortDirection, int page, int pageSize, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> SelectIncludesOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> order, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TObject>> SelectObjectWithIncludes<TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TObject>> SelectObjectWithIncludes<TKey, TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> order, System.ComponentModel.ListSortDirection sortDirection, params Expression<Func<TEntity, object>>[] includes);

        Task<int> Add(TEntity entity);

        Task<TEntity> AddAndReturn(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entityList);

        Task<int> Update(TEntity entity);

        Task UpdateEntity(TEntity entitySource, TEntity entityDestiny);

        Task<TEntity> UpdateKeyAndReturn(TEntity entity, object key);

        Task Delete(TEntity entity);

        Task<int> DeleteAndReturn(TEntity entity);

        Task DeleteRange(IEnumerable<TEntity> entityList);

        void Attach(TEntity entity);

        IQueryable<TEntity> GetQueryable();

        Task<int> Count();

        Task<int> CountIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TObject> FirstOrDefaultSelect<TObject>(Expression<Func<TEntity, TObject>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FirstOrDefaultOrder(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> order, System.ComponentModel.ListSortDirection sortDirection, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> ExecuteQuery<TTEntity>(string query, List<System.Data.SqlClient.SqlParameter> parameters);
    }
}
