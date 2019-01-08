using Infrastructure.providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        private ISQLSearchProvider SearchProvider;
        public GenericRepository(DbContext context, ISQLSearchProvider provider)
        {
            this.SearchProvider = provider;
            this.context = context;
            this.dbSet = context.Set<T>();
         }

   
        public virtual IQueryable<T> Get(string EntityId, string UserId, string BucketId,
                     APISearch Query)
        {
            string sqls = SearchProvider.GetQuery(Query);
            IQueryable<T> query;
            if (sqls != "")
            {
                DataTable dt = new DataTable();
                var conn = context.Database.Connection;
                var connectionState = conn.State;

                if (connectionState == ConnectionState.Closed) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqls;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("", ""));
                    cmd.Connection = conn;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                if (connectionState == ConnectionState.Closed) conn.Close();


                Type tipo = typeof(T);
                IList lista = (IList)Activator.CreateInstance((typeof(List<>).MakeGenericType(tipo)));

                foreach (DataRow r in dt.Rows)
                {
                    object o = Activator.CreateInstance(tipo);
                    var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    foreach (var property in properties)
                    {


                        foreach (DataColumn c in dt.Columns)
                        {

                            if (c.ColumnName.ToLower() == property.Name.ToLower())
                            {
                                try
                                {
                                    if (r[c.ColumnName] != System.DBNull.Value)
                                    {

                                        if (c.DataType == System.Type.GetType("System.DateTime"))
                                        {
                                            DateTime d = Convert.ToDateTime(r[c.ColumnName]).AddHours(int.Parse(Query.gmt));
                                            property.SetValue(o, d);
                                        }
                                        else {
                                            property.SetValue(o, r[c.ColumnName]);
                                        }
                                        
                                    }

                                }
                                catch (Exception ex)
                                {



                                }
                            }
                        }


                    }

                    lista.Add(o);
                }

                query = (IQueryable<T>)lista.AsQueryable();


            }
            else {
                query = null;
            }
            

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

        public int GetTotalCount(string EntityId, string UserId, string BucketId, APISearch Query)
        {
            throw new NotImplementedException();
        }

        public int GetFilteredCount(string EntityId, string UserId, string BucketId, APISearch Query)
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

        public IQueryable<T> GetEF(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {

          
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();

            }
            else
            {
                return query.AsQueryable(); 
            }

 

        }
    }
}
