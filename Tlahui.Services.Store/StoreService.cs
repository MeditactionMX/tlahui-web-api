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
using Tlahui.Domain.Base.Entities;
using Tlahui.Services.Caching.simple;
using Tlahui.Domain.Common.Entities;

namespace Tlahui.Services.Store
{
    public class StoreService : IStoreService, IDisposable
    {
        public const string KEY_CATEGORIES = "Tlahui.Domain.Store.Entities.Category";
        public const string KEY_CATEGORIES_BASEQUERY = "KEY_CATEGORIES_BASEQUERY";
        public const string CATEGORIES_DEFAULT_SORT_COLUMN = "Name";
        public const string CATEGORIES_DELETED_MARK_COLUMN = "Deleted";
        

        private ICachedResourceStatisticsRepository CachedResourceStatisticsRepository;
        private ICategoriesRepository CategoriesRepository;
        private IDynamicFormsRepository DynamicFormsRepository;
        private ICacheService CacheService;
        private WebAPIContext context;

        public string BucketId { get; set; }
        public string UserId { get; set; }


        public StoreService(ICategoriesRepository CategoriesRepository, 
            IDynamicFormsRepository DynamicFormsRepository,
            ICachedResourceStatisticsRepository CachedResourceStatisticsRepository,
            ICacheService CacheService,
            DbContext context)
        {
            this.CachedResourceStatisticsRepository = CachedResourceStatisticsRepository;
            this.CategoriesRepository = CategoriesRepository;
            this.DynamicFormsRepository = DynamicFormsRepository;
            this.CacheService = CacheService;
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


        private string GetBaseQuery(string ResourceId) {
           return context.EntityQueries.Where(x => x.ResourceId == ResourceId).Select(x=>x.Query).First();
        }


        #region CategoriesCache

        public string GetDefaultCategoriesSortColumn()
        {
            return CATEGORIES_DEFAULT_SORT_COLUMN;
        }

        public string GetDefaultCategoriesDeletedMarkColum()
        {
            return CATEGORIES_DELETED_MARK_COLUMN;
        }

        #endregion

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

        public IQueryable<Category> GetCategories( APISearch Query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            if (string.IsNullOrEmpty(Query.sortby)) Query.sortby = this.CacheService.GetOrSet<string>("CATEGORIES_DEFAULT_SORT_COLUMN", () => GetDefaultCategoriesSortColumn(), 1440); 
            if (string.IsNullOrEmpty(Query.markdeletedfield)) Query.markdeletedfield = this.CacheService.GetOrSet<string>("CATEGORIES_DELETED_MARK_COLUMN", () => GetDefaultCategoriesDeletedMarkColum(), 1440);
            Query.basequery = this.CacheService.GetOrSet<string>(KEY_CATEGORIES_BASEQUERY, () => GetBaseQuery(KEY_CATEGORIES), 1440);

            return this.CategoriesRepository.Get(KEY_CATEGORIES, this.UserId, this.BucketId, Query);
        }

        public int GetCategoriesFilteredCount(APISearch Query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            if (Query.sortby == "") Query.sortby = CATEGORIES_DEFAULT_SORT_COLUMN;
            
            return this.CategoriesRepository.GetFilteredCount(KEY_CATEGORIES, this.UserId, this.BucketId, Query);
        }

        public int GetCategoriesTotalCount(APISearch Query)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            if (Query.sortby == "") Query.sortby = CATEGORIES_DEFAULT_SORT_COLUMN;
            return this.CategoriesRepository.GetTotalCount(KEY_CATEGORIES, this.UserId, this.BucketId, Query);
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


        public List<APIKeyValuePair> CategoriesCatalog(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return CategoriesRepository.GetEF(filter: f => f.Name.Contains(filter) && f.BucketId == BucketId,
                orderBy: o => o.OrderBy(d => d.Name)).Select(
                c => new APIKeyValuePair()
                {
                    Key = c.Id.ToString(),
                    Value = c.Name
                }
                ).ToList();
            }
            else {
                return CategoriesRepository.GetEF(filter: f => f.BucketId == BucketId,
                orderBy: o => o.OrderBy(d => d.Name)).Select(
                c => new APIKeyValuePair()
                {
                    Key = c.Id.ToString(),
                    Value = c.Name
                }
                ).ToList();
            }

            
        }

        #endregion


        #region DynamicUI

        public Task<UITable> GetCategoryUITable(string language, string locale)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            //Este cache solo va a cambiar durante actualizaciones a la DB y sólo ocurriran 
            //de manera manual o en migraciones lo que reiniciará el cache
            return this.CacheService.GetOrSet<Task<UITable>>("GetCategoryUITable", () => DynamicFormsRepository.GetTableMetadata(KEY_CATEGORIES, language, locale), 1440);
        }


        public Task<UIForm> GetCategoryUIForm(string language, string locale)
        {
            if (!AllowExecute()) throw new Exception("Forbidden");
            //Este cache solo va a cambiar durante actualizaciones a la DB y sólo ocurriran 
            //de manera manual o en migraciones lo que reiniciará el cache
            return this.CacheService.GetOrSet<Task<UIForm>>("GetCategoryUIForm", () => DynamicFormsRepository.GetFormMetadata(KEY_CATEGORIES, language, locale), 1440);
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
