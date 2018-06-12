using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{
    public interface ILocalizable
    {
         string Language { get; set; }

         string Culture { get; set; }

        string Traslation { get; set; }

        string Context { get; set; }

        string TraslationId { get; set; }

        string Plural { get; set; }
    }
}
