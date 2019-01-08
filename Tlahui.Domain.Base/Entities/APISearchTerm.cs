using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Base.Entities
{
    public class APISearchTerm
    {
        public APISearchTerm() {
            this._values = new List<string>();

        }



        public string field { get; set; }

        public bool not { get; set; }

        public APISearch.SearchComparer op { get; set; }

        public Shared.DataType type { get; set; }


        private List<string> _values;

        public List<string> values
        {
            get { return _values; }
            set { _values = value; }
        }
        
    }
}
