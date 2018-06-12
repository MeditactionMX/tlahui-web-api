using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{

    public enum SortOrder {
        database=0, ascending=1, descending=2
    }

    public class RepositoryQuery
    {

        public RepositoryQuery() {
            this.Filters = new List<FilterAttribute>();
            this.Sorting = new List<SortAttribute>();
            this.Columns = new List<SearchResultColumn>();
            this.PageNumber = 1;
            this.PageSize = 25;
        }

        public List<FilterAttribute> Filters { get; set; }

        public List<SortAttribute> Sorting { get; set; }

        public List<SearchResultColumn> Columns { get; set; }


        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
