using Infrastructure;
using Infrastructure.GenericRepository;
using Infrastructure.providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Store.Entities;

namespace Tlahui.Repositories.Store
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {


        public CategoriesRepository(DbContext Db, ISQLSearchProvider provider):base(Db, provider)
        {
            }

    }
}
