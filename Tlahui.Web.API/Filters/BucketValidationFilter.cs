using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Tlahui.Web.API.Filters
{

    [AttributeUsage(AttributeTargets.All)]
    public class BucketValidationFilter: ActionFilterAttribute
    {

        public BucketValidationFilter() {

        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            string bucket = await IsValidRequestAsync(actionContext.Request);

            if (bucket == "")
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden,
                    "Bucket information missing"
                );
            }


            actionContext.Request.Properties.Add("BucketId", bucket);
            actionContext.RequestContext.RouteData.Values.Add("BucketId", bucket);
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private async Task<string> IsValidRequestAsync(HttpRequestMessage request)
        {
            var headerExists = request.Headers.TryGetValues(
                "X-Tlahui-Bucket", out IEnumerable<string> signature);
            if (!headerExists) return "";


            return await Task.FromResult(signature.First());

            //var requestUrl = _urlSchemeAndDomain + request.RequestUri.PathAndQuery;
            //var formData = await GetFormDataAsync(request.Content);
            //return new RequestValidator(_authToken).Validate(requestUrl, formData, signature.First());
        }

        private async Task<IDictionary<string, string>> GetFormDataAsync(HttpContent content)
        {
            string postData;
            using (var stream = new StreamReader(await content.ReadAsStreamAsync()))
            {
                stream.BaseStream.Position = 0;
                postData = await stream.ReadToEndAsync();
            }

            if (!String.IsNullOrEmpty(postData) && postData.Contains("="))
            {
                return postData.Split('&')
                    .Select(x => x.Split('='))
                    .ToDictionary(
                        x => Uri.UnescapeDataString(x[0]),
                        x => Uri.UnescapeDataString(x[1].Replace("+", "%20"))
                    );
            }

            return new Dictionary<string, string>();
        }
    }
}