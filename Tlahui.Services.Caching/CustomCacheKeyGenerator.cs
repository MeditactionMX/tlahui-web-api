using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Tlahui.Services.Caching
{
    public class CustomCacheKeyGenerator : WebApi.OutputCache.V2.DefaultCacheKeyGenerator
    {
        public override string MakeCacheKey(HttpActionContext actionContext, MediaTypeHeaderValue header, bool excludeQueryString = false)
        {
            object bucketId;
            if (actionContext.Request.Properties.TryGetValue("BucketId", out bucketId))
            {
                return bucketId + "-" + base.MakeCacheKey(actionContext, header, excludeQueryString);
            }
            else {
                return base.MakeCacheKey(actionContext, header, excludeQueryString);
            }
 
           
            
        }
    }
}
