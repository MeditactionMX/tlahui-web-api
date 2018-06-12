using DynamicForms.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Store.Entities;

namespace Tlahui.Services.Store
{
    public interface IStoreService: IAutenticatedService
    {



        #region Categories

        IQueryable<Category> GetCategories(RepositoryQuery query);

        int GetCategoriesTotalCount(RepositoryQuery query);

        int GetCategoriesFilteredCount(RepositoryQuery query);

        Task<Category> GetCategoryByID(object id);

        Task<Category> InsertCategory(Category entity);

        void UndeleteCategory(Category entityToDelete);

        void DeleteCategory(Category entityToDelete);

        Task<Category> UpdateCategory(Category entityToUpdate);

        Task<UITable> GetCategoryUITable(string language, string locale);

        bool IsValidSession();


        void UpdateCategoryStats();

        DateTime GetCategoriesLastUpdate();

        #endregion


    }
}
