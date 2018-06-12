using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Tlahui.Web.API.Helpers
{
    public static class BucketHelper
    {
        public static string GetBucketId(HttpRequestMessage request) {

            string id = "";

            object bucketId;
            if (request.Properties.TryGetValue("BucketId", out bucketId))
            {
                id = (string)bucketId;
            }
            else
            {
                id = "DemoBucket";
            }

            return id;
        }

    }
}