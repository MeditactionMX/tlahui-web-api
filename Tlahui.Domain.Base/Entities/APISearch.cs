using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Base.Entities
{
    public class APISearch
    {

        public enum SearchComparer
        {
            none = 0, eq = 1, gt = 2, lt = 3, gte = 4, lte = 5, starts = 10, ends = 11, contains = 12, @in=20, between = 30
        }



        public APISearch() {
            this._filters = new List<APISearchTerm>();
            this.page = 0;
            this.pagesize = 25;
            this.sortby = "";
            this.sortdirection = "asc";
            this.datetimeasdate = true;
            this.gmt = "-6";
            this.lang = "es-MX";
            this.ShowDeleted = false;
           
        }

        public string basequery { get; set; }

        public int page { get; set; }
        public int pagesize { get; set; }

        public string sortby { get; set; }

        public string sortdirection { get; set; }

        public bool datetimeasdate { get; set; }

        public string gmt { get; set; }

        public string lang { get; set; }

        public bool ShowDeleted { get; set; }

        public string markdeletedfield { get; set; }

        private List<APISearchTerm> _filters;

        public List<APISearchTerm> filters
        {
            get { return _filters; }
            set { _filters = value; }
        }


    }
}
