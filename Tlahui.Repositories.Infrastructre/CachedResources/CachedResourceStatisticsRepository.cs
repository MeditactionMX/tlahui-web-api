using Infrastructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Infraestructure.Entities;

namespace Tlahui.Repositories.Infrastructure.CachedResources
{

    public class CachedResourceStatisticsRepository : GenericRepository<CachedResourceStatistics>, ICachedResourceStatisticsRepository
    {
        private Tlahui.Context.WebAPI.WebAPIContext context;
        public CachedResourceStatisticsRepository(DbContext Db) : base(Db)
        {
            this.context = (Tlahui.Context.WebAPI.WebAPIContext)Db;
        }

        public CachedResourceStatistics FimdById(string Key, string BucketId)
        {
            return this.context.CachedResourceStatistics.Where(x => x.BucketId == BucketId && x.ResourceKey == Key).SingleOrDefault();
        }


    }

}
