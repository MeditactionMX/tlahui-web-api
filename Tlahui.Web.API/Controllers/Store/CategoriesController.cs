using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tlahui.Domain.Store.Entities;
using Tlahui.Services.Store;
using System.Web.Http.Cors;
using WebApi.OutputCache.V2;
using Tlahui.Web.API.Filters;
using Tlahui.Web.API.Helpers;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Security.Claims;
using Infrastructure;
using DynamicForms.Entities;
using System.Configuration;

namespace Tlahui.Web.API.Controllers.Store
{
    [Authorize]
    [BucketValidationFilter]
    [RoutePrefix("api/store/categories")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {

        private IStoreService StoreService;
        public CategoriesController(IStoreService StoreService)
        {
            this.StoreService = StoreService;
        }


        #region Setup
        private void SetSecurityInfo()
        {
            this.StoreService.BucketId = BucketHelper.GetBucketId(this.Request);
            this.StoreService.UserId = GetUserID();

        }

        public const string objectIdElement = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public const string upnIdElement = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

        private string GetUserID()
        {
            try
            {
                return ClaimsPrincipal.Current.FindFirst(objectIdElement).Value;
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["demouser"])) {
                    return ConfigurationManager.AppSettings["demouser"];
                } else { 
                    return "";
                }
            }

        }

        private string GetLanguage() {
            var langHeaderExists = this.ActionContext.Request.Headers.TryGetValues(
           "Accept-Language", out IEnumerable<string> signature);
            if (!langHeaderExists)
            {
                return "es-MX";

            }
            else
            {
                return signature.First();
            }
        }

        #endregion


        // GET: api/store/categories      
        [CacheOutput(ClientTimeSpan = 180, ServerTimeSpan = 100, AnonymousOnly = true)]
        [Route("")]
        [HttpGet]
        public IQueryable<Category> GetCategories()
        {   

            SetSecurityInfo();
            return this.StoreService.GetCategories(new Infrastructure.RepositoryQuery());
        }


        // GET: api/store/categories/id
        [ResponseType(typeof(Category))]
        [Route("{id:regex(\\w)}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCategory(string id)
        {
            SetSecurityInfo();

            Category category = await StoreService.GetCategoryByID(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }



        // POST: api/store/categories
        [ResponseType(typeof(Category))]
        [Route("", Name ="CategoryPost")]
        [HttpPost]
        public async Task<IHttpActionResult> PostCategory([FromBody] Category category)
        {
            SetSecurityInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                category = await StoreService.InsertCategory(category);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return CreatedAtRoute("CategoryPost", new { id = category.Id }, category);
        }


        // PUR: api/store/categories
        [ResponseType(typeof(void))]
        [Route("{id:regex(\\w)}", Name = "CategoryPut")]
        [HttpPut]
        public async Task<IHttpActionResult> PutCategory(string id,[FromBody] Category category)
        {

            SetSecurityInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                await this.StoreService.UpdateCategory(category);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        // DELETE: api/Store/Categories/id
        [ResponseType(typeof(Category))]
        [HttpDelete]
        [Route("{id:regex(\\w)}", Name = "CategoryDelete")]
        public async Task<IHttpActionResult> DeleteCategory(string id)
        {
            SetSecurityInfo();
            Category category = await StoreService.GetCategoryByID(id);
            if (category == null)
            {
                return NotFound();

            }
            try
            {
                StoreService.DeleteCategory(category);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
         

            return Ok(category);
        }


        // DELETE: api/Store/Categories/id
        [ResponseType(typeof(Category))]
        [HttpPatch]
        [Route("undelete/{id:regex(\\w)}", Name = "CategoryUndelete")]
        public async Task<IHttpActionResult> UndeleteCategory(string id)
        {
            SetSecurityInfo();
            Category category = await StoreService.GetCategoryByID(id);
            if (category == null)
            {
                return NotFound();

            }
            try
            {
                StoreService.UndeleteCategory(category);
            }
            catch (Exception)
            {

                return InternalServerError();
            }


            return Ok(category);
        }


        [ResponseType(typeof(UITable))]
        [HttpGet]
        [Route("metadata/table", Name = "CategoryUITable")]
        public async Task<IHttpActionResult> GetUITable()
        {
            SetSecurityInfo();
            List<string> LangParts = this.GetLanguage().Split('-').ToList();
            if (LangParts.Count == 1) {
                LangParts.Add("");
            }

            UITable table = await StoreService.GetCategoryUITable(LangParts[0], LangParts[1]);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

    }
}