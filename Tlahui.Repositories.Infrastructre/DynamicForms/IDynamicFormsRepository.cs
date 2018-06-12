using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicForms.Entities;

namespace Tlahui.Repositories.Infrastructure.DynamicForms
{
    public interface IDynamicFormsRepository
    {
        Task<UITable> GetTableMetadata(string ResourceId, string Language, string Locale);
    }
}
