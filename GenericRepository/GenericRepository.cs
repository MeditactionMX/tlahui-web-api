using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;

namespace Infrastructure.GenericRepository
{
    public class GenericRepository<T> : 
       IGenericRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
         }


        public virtual IQueryable<T> Get(string UserId, string BucketId,
      RepositoryQuery Query)
        {
            IQueryable<T> query = dbSet;

            return query;
        }


        public virtual T GetByID(string UserId, string BucketId, object id)
        {

            return dbSet.Find(id);
        }

        public virtual void UnDelete(string UserId, string BucketId, T entityToDelete)
        {
            if (entityToDelete is ITrackableEntity)
            {
                ((ITrackableEntity)(object)entityToDelete).Deleted = false;
                ((ITrackableEntity)(object)entityToDelete).UpdateDate = DateTime.UtcNow;
                ((ITrackableEntity)(object)entityToDelete).ModifierId = UserId;

                dbSet.Attach(entityToDelete);
                context.Entry(entityToDelete).State = EntityState.Modified;

            }

        }


        public virtual void Delete(string UserId, string BucketId, T entityToDelete)
        {
            if (entityToDelete is ITrackableEntity)
            {
                ((ITrackableEntity)(object)entityToDelete).Deleted = true;
                ((ITrackableEntity)(object)entityToDelete).DeletedDate = DateTime.UtcNow;
                ((ITrackableEntity)(object)entityToDelete).DeleterId = UserId;
                dbSet.Attach(entityToDelete);
                context.Entry(entityToDelete).State = EntityState.Modified;

            }
            else {
                //Remueve de la base de datos
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            
        }
 

        //-----------------------------------

        public int GetTotalCount(string UserId, string BucketId, RepositoryQuery Query)
        {
            throw new NotImplementedException();
        }

        public int GetFilteredCount(string UserId, string BucketId, RepositoryQuery Query)
        {
            throw new NotImplementedException();
        }

        T IGenericRepository<T>.Insert(string UserId, string BucketId, T entity, bool SaveNow)
        {
            if (entity is ITrackableEntity)
            {
                ((ITrackableEntity)(object)entity).CreatorId = UserId;
                ((ITrackableEntity)(object)entity).ModifierId = UserId;
            }

            if (entity is IBucketTrackable)
            {
                ((IBucketTrackable)(object)entity).BucketId = BucketId;
            }

            dbSet.Add(entity);
            if (SaveNow) {
                Save();
            }
            return entity;
        }

        T IGenericRepository<T>.Update(string UserId, string BucketId, T entityToUpdate)
        {

            if (entityToUpdate is ITrackableEntity)
            {
                ((ITrackableEntity)(object)entityToUpdate).ModifierId = UserId;
                ((ITrackableEntity)(object)entityToUpdate).UpdateDate = DateTime.UtcNow;
            }

            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
