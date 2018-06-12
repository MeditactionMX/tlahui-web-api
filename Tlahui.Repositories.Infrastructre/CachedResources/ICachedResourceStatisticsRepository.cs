using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Infraestructure.Entities;

namespace Tlahui.Repositories.Infrastructure.CachedResources
{

    public interface ICachedResourceStatisticsRepository : IGenericRepository<CachedResourceStatistics>
    {

        CachedResourceStatistics FimdById(string Key, string BucketId);
    }
 
}
