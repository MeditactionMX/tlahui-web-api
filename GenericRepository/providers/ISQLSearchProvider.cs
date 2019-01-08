using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;

namespace Infrastructure.providers
{
    public interface ISQLSearchProvider
    {
        string GetQuery(APISearch Search);
    }
}
