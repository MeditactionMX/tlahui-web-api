using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Store.Entities;

namespace Tlahui.Repositories.Store
{
    public interface ICategoriesRepository: IGenericRepository<Category>
    {
    }
}
