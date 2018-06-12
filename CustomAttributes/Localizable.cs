using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{

    [System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class Localizable: System.Attribute, ILocalizable
    { 
 
        public string Language { get; set; }
        public string Culture { get; set;}
        public string Traslation { get; set; }
        public string Context { get; set; }
        public string TraslationId { get; set; }
        public string Plural { get; set; }


        /// <summary>
        /// No default language
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="Culture"></param>
        /// <param name="Traslation"></param>
        /// <param name="Plural"></param>
        public Localizable(string Language, string Culture, string Traslation, string Plural) {
            this.Language = Language;
            this.Culture = Culture;
            this.Traslation = Traslation;
            this.Context = "";
            this.TraslationId = System.Guid.NewGuid().ToString();
            this.Plural = Plural;
        }


        /// <summary>
        /// Default language es-MX
        /// </summary>
        /// <param name="Traslation"></param>
        public Localizable(string Traslation, string Plural)
        {
            this.Language = "es";
            this.Culture = "MX";
            this.Traslation = Traslation;
            this.Context = "";
            this.Plural = Plural;
            this.TraslationId = System.Guid.NewGuid().ToString();
        }


    }
}
