using DynamicForms.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;
using Tlahui.Domain.Common.Entities;
using Tlahui.Domain.Store.Entities;

namespace Tlahui.Services.Store
{
    public interface IStoreService: IAutenticatedService
    {



        #region Categories

        string GetDefaultCategoriesSortColumn();

        IQueryable<Category> GetCategories(APISearch Query);

        int GetCategoriesTotalCount(APISearch Query);

        int GetCategoriesFilteredCount(APISearch Query);

        Task<Category> GetCategoryByID(object id);

        Task<Category> InsertCategory(Category entity);

        void UndeleteCategory(Category entityToDelete);

        void DeleteCategory(Category entityToDelete);

        Task<Category> UpdateCategory(Category entityToUpdate);

        Task<UITable> GetCategoryUITable(string language, string locale);

        Task<UIForm> GetCategoryUIForm(string language, string locale);

        bool IsValidSession();


        void UpdateCategoryStats();

        DateTime GetCategoriesLastUpdate();

        List<APIKeyValuePair> CategoriesCatalog(string filter);

        #endregion


    }
}
