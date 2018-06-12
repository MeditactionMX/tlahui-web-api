using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Repositories.Store;
using System.Linq.Expressions;
using Tlahui.Domain.Store.Entities;
using Tlahui.Context.WebAPI;
using System.Data.Entity;
using Tlahui.Repositories.Infrastructure.CachedResources;
using Tlahui.Domain.Infraestructure.Entities;
using Tlahui.Repositories.Infrastructure.DynamicForms;
using DynamicForms.Entities;

namespace Tlahui.Services.Store
{
    public class StoreService : IStoreService, IDisposable
    {
        public const string KEY_CATEGORIES = "Tlahui.Domain.Store.Entities.Category"; 


        private ICachedResourceStatisticsRepository CachedResourceStatisticsRepository;
        private ICategoriesRepository CategoriesRepository;
        private IDynamicFormsRepository DynamicFormsRepository;
        private WebAPIContext context;

        public string BucketId { get; set; }
        public string UserId { get; set; }


        public StoreService(ICategoriesRepository CategoriesRepository, 
            IDynamicFormsRepository DynamicFormsRepository,
            ICachedResourceStatisticsRepository CachedResourceStatisticsRepository,  
            DbContext context)
        {
            this.CachedResourceStatisticsRepository = CachedResourceStatisticsRepository;
            this.CategoriesRepository = CategoriesRepository;
            this.DynamicFormsRepository = DynamicFormsRepository;
            this.context = (WebAPIContext)context;
        }



        public bool IsValidSession()
        {
            if (!(string.IsNullOrEmpty(this.BucketId)) && !(string.IsNullOrEmpty(this.UserId)))
            {
                return true;
            }
            else {
                return false;
            }
        }


        public bool AllowExecute() {

            return IsValidSession();

        }

        #region Categories



        public void DeleteCategory(Category entityToDelete)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");

            this.CategoriesRepository.Delete(this.UserId, this.BucketId, entityToDelete);
            this.CategoriesRepository.Save();
        }


        public void UndeleteCategory(Category entityToUndelete)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");

            this.CategoriesRepository.UnDelete(this.UserId, this.BucketId, entityToUndelete);
            this.CategoriesRepository.Save();
        }

        public IQueryable<Category> GetCategories( RepositoryQuery query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            return this.CategoriesRepository.Get(this.UserId, this.BucketId, query);
        }

        public int GetCategoriesFilteredCount(RepositoryQuery query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            return this.CategoriesRepository.GetFilteredCount(this.UserId, this.BucketId, query);
        }

        public int GetCategoriesTotalCount(RepositoryQuery query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            return this.CategoriesRepository.GetTotalCount(this.UserId, this.BucketId, query);
        }

        public Task<Category> GetCategoryByID(object id)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            return Task.FromResult(this.CategoriesRepository.GetByID(this.UserId, this.BucketId, id));
        }

        public Task<Category> InsertCategory(Category entity)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            entity =CategoriesRepository.Insert(this.UserId, this.BucketId, entity, true);
            return Task.FromResult(entity);
        }

        public Task<Category> UpdateCategory(Category entityToUpdate)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            Category c = GetCategoryByID(entityToUpdate.Id).Result;

            if (c == null) {
                return null;
            }

            c.Description = entityToUpdate.Description;
            c.DisplayOrder = entityToUpdate.DisplayOrder;
            c.KeyWords = entityToUpdate.KeyWords;
            c.Name = entityToUpdate.Name;
            c.ParentCategoryId = entityToUpdate.ParentCategoryId;
            c.Published = entityToUpdate.Published;

            CategoriesRepository.Update(this.UserId, this.BucketId, c);
            this.Save();
            return Task.FromResult(c);
        }


        public void UpdateCategoryStats()
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            CachedResourceStatistics s = this.CachedResourceStatisticsRepository.FimdById(KEY_CATEGORIES, BucketId);
            if (s == null)
            {
                s = AddCategoryStats();
            }
            else {
                s.LastUpdated = DateTime.UtcNow;
                this.CachedResourceStatisticsRepository.Update(this.UserId, this.BucketId, s);
            }
        }

        public DateTime GetCategoriesLastUpdate()
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            CachedResourceStatistics s = this.CachedResourceStatisticsRepository.FimdById(KEY_CATEGORIES, BucketId);
            if (s == null)
            {
               s= AddCategoryStats();
            }

            return s.LastUpdated;
        }


        private CachedResourceStatistics AddCategoryStats() {
            CachedResourceStatistics s = new CachedResourceStatistics() { Count = 0,
                LastUpdated = DateTime.UtcNow, ResourceKey = KEY_CATEGORIES, BucketId  = BucketId };
            s = this.CachedResourceStatisticsRepository.Insert(this.UserId, this.BucketId, s, true);
            return s;
        }


        #endregion


        #region DynamicUI

        public Task<UITable> GetCategoryUITable(string language, string locale)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            return DynamicFormsRepository.GetTableMetadata(KEY_CATEGORIES, language, locale);
        }


        #endregion


        #region unit of work

        public void Save()
        {
            context.SaveChanges();
        }

        #endregion




        #region Disposing
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

     




        #endregion
    }
}
