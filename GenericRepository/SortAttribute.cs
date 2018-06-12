using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SortAttribute
    {

        public string Name { get; set; }
        public int Index { get; set; }
        public SortOrder Order { get; set; }

    }
}
