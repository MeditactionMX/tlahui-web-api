using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicForms.Entities;
using Tlahui.Domain.Infraestructure.Entities;

namespace Tlahui.Repositories.Infrastructure.DynamicForms
{
    public class DynamicFormsRepository : IDynamicFormsRepository
    {

        private const string DEFAULT_LANG = "es";
        private const string DEFAULT_LOCALE = "MX";

        private Tlahui.Context.WebAPI.WebAPIContext context;
        public DynamicFormsRepository(DbContext Db)
        {
            this.context = (Tlahui.Context.WebAPI.WebAPIContext)Db;
        }

        public Task<UITable> GetTableMetadata(string ResourceId, string Language, string Locale)
        {
            UITable table = new UITable();
            List<LocalizableResource> list = 
            context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
            x.Language == Language && x.Culture == Locale).ToList();

            if (list.Count == 0) {
                list = context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
                x.Language == DEFAULT_LANG && x.Culture == DEFAULT_LOCALE).ToList();
            }

            foreach (LocalizableResource r in list) {
                if (r.ResourceId == r.ResourceGroupId)
                {
                    table.Traslation = r.Traslation;
                    table.Plural = r.Plural;
                    table.Language = r.Language;
                    table.Culture = r.Culture;
                    table.ShortId = r.ShortId;
                    
                }
                else {
                    UIColumn col = new UIColumn();
                    col.Traslation = r.Traslation;
                    col.Plural = r.Plural;
                    col.Language = r.Language;
                    col.Culture = r.Culture;
                    col.ShortId = r.ShortId;
                    table.Columns.Add(col);
                }
            }
            

            return Task.FromResult(table);

        }
    }
}
