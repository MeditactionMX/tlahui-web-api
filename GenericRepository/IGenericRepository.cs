﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> Get(string UserId,string BucketId, RepositoryQuery Query);

        int GetTotalCount(string UserId, string BucketId, RepositoryQuery Query);

        int GetFilteredCount(string UserId, string BucketId, RepositoryQuery Query);

        T GetByID(string UserId, string BucketId, object id);

        T Insert(string UserId, string BucketId, T entity, bool SaveNow);

        void Delete(string UserId, string BucketId, T entityToDelete);

        void UnDelete(string UserId, string BucketId, T entityToDelete);

        T Update(string UserId, string BucketId, T entityToUpdate);

        void Save();
    }
}
