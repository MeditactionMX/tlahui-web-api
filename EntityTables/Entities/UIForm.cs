using CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicForms.Entities
{
    public class UIForm : ILocalizable
    {
        public UIForm()
        {
            FormFields = new List<UIFormField>();
        }

        public string Language { get; set; }
        public string Culture { get; set; }
        public string Traslation { get; set; }
        public string Context { get; set; }
        public string TraslationId { get; set; }
        public string Plural { get; set; }
        public string ResourceId { get; set; }
        public string ShortId { get; set; }
        public List<UIFormField> FormFields { get; set; }
   
    }
}
