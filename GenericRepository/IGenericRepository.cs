using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;

namespace Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {


        IQueryable<T> GetEF(Expression<Func<T, bool>> filter = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        IQueryable<T> Get(string EntityId, string UserId,string BucketId, APISearch Query);

        int GetTotalCount(string EntityId, string UserId, string BucketId, APISearch Query);

        int GetFilteredCount(string EntityId, string UserId, string BucketId, APISearch Query);

        T GetByID(string UserId, string BucketId, object id);

        T Insert(string UserId, string BucketId, T entity, bool SaveNow);

        void Delete(string UserId, string BucketId, T entityToDelete);

        void UnDelete(string UserId, string BucketId, T entityToDelete);

        T Update(string UserId, string BucketId, T entityToUpdate);

        void Save();
    }
}
