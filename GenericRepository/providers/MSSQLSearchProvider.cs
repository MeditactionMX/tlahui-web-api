using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;
using Tlahui.Domain.Shared;
namespace Infrastructure.providers
{
    public class MSSQLSearchProvider: ISQLSearchProvider
    {
        public string GetQuery(APISearch Search) {

            string query = "";
            string where = " where (1=1) ";
            string sort = $" order by  {Search.sortby} {Search.sortdirection} ";
            string paging = "";

            if (Search.pagesize != 0) {
                paging = $" OFFSET { Search.page * Search.pagesize } ROWS FETCH NEXT { Search.pagesize } ROWS ONLY ";
            }

            bool detetedfieldincluded = false;

            if (!string.IsNullOrEmpty(Search.basequery)) {
                query = $"select * from ({Search.basequery}) as data \r\n";
               
                foreach (var f in Search.filters) {
   
                    if (f.values.Count > 0) {
                      
                        string w = this.GetFilterAsString(f, int.Parse(Search.gmt), Search.datetimeasdate);
                        if (w != "")
                        {
                            //Determina si aplica el filtro automático de eliminados
                            if (f.field.ToLower() == Search.markdeletedfield.ToLower()) {
                                detetedfieldincluded = true;
                            }
                            if (where != "") where += " AND ";
                            where += w;
                        }
                    }
                }

                if (!detetedfieldincluded) {
                    if (!Search.ShowDeleted)
                    {
                        if (where != "") where += " AND ";
                        where += $" ({Search.markdeletedfield} =0 ) ";
                    }
                }
                 
            }

            string finalQuery = query + "\r\n" + where + "\r\n" + sort + "\r\n" + paging;

            

            return finalQuery;
        }


        private string GetFilterAsString(APISearchTerm term, int offset, bool datetimeasdate) {
            string filter = "";

            switch (term.type)
            {
                case DataType.date:
                    filter = GetDateFilterAsString(term, offset);
                    break;

                case DataType.datetime:
                    filter = GetDatetimeFilterAsString(term, offset, datetimeasdate);
                    break;


                case DataType.time:
                    filter = GetTimeFilterAsString(term);
                    break;

                case DataType.decimal_number:
                    filter = GetDecimalNumberFilterAsString(term);
                    break;

                case DataType.integer_number:
                    filter = GetIntegerNumberFilterAsString(term);
                    break;

                case DataType.boolean:
                    filter = GetBooleanFilterAsString(term);
                    break;

                case DataType.text:
                    filter = GetTextFilterAsString(term);
                    break;


            }
            return filter;
        }

        private string GetTextFilterAsString(APISearchTerm term)
        {
            string filter = "";
            string not = "";
            switch (term.op)
            {


                case APISearch.SearchComparer.contains:
                    foreach (string v in term.values) {
                        if (filter != "") {
                            filter += " OR ";
                        }

                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} like '%{v}%') ";
                    }
                    break;

                case APISearch.SearchComparer.ends:
                    foreach (string v in term.values)
                    {
                        if (filter != "")
                        {
                            filter += " OR ";
                        }

                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} like '%{v}') ";
                    }
                    break;

                case APISearch.SearchComparer.eq:
                    foreach (string v in term.values)
                    {
                        if (filter != "")
                        {
                            filter += " OR ";
                        }
                        not = term.not ? "<>" : "=";
                        filter += $" ({term.field} {not} '{v}') ";
                    }
                    break;

                case APISearch.SearchComparer.@in:
                    string inf = "";
                    foreach (string v in term.values)
                    {
                        inf += $"'{inf}',";
                    }
                    inf = inf.TrimEnd(',');
                    not = term.not ? "not" : "=";
                    filter += $" ({term.field} {not} in '{inf}') ";
                    break;

                case APISearch.SearchComparer.starts:
                    foreach (string v in term.values)
                    {
                        if (filter != "")
                        {
                            filter += " OR ";
                        }
                        not = term.not ? "not" : "=";
                        filter += $" ({term.field} {not} like '{v}%') ";
                    }
                    break;
 

            }

            return filter;
        }


        private string GetBooleanFilterAsString(APISearchTerm term)
        {
            string filter = "";
            string not = "";
            string value = "";
            switch (term.op)
            {

                case APISearch.SearchComparer.eq:
                    foreach (string v in term.values)
                    {
                        if (filter != "")
                        {
                            filter += " OR ";
                        }

                        value = (v.ToLower() == "true") ? "1" : "0";
                        not = term.not ? "<>" : "=";
                        filter += $" ({term.field} {not} {value}) ";
                    }
                    break;
                    
            }

            return filter;
        }


        private string GetIntegerNumberFilterAsString(APISearchTerm term)
        {
        
            string filter = "";
            string not = "";
            switch (term.op)
            {

                case APISearch.SearchComparer.between:
                    if (term.values.Count == 2)
                    {
                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} between {term.values[0]} and  {term.values[1]} ) ";
                    }
                    break;

                case APISearch.SearchComparer.eq:
                    not = term.not ? "<>" : "=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.gt:
                    not = term.not ? "<=" : ">";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.gte:
                    not = term.not ? "<" : ">=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;


                case APISearch.SearchComparer.lt:
                    not = term.not ? ">=" : "<";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.lte:
                    not = term.not ? ">" : "<=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

             

            }

            return filter;
        }

        private string GetDecimalNumberFilterAsString(APISearchTerm term)
        {
            string filter = "";
            string not = "";
            switch (term.op)
            {

                case APISearch.SearchComparer.between:
                    if (term.values.Count == 2)
                    {
                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} between {term.values[0]} and  {term.values[1]} ) ";
                    }
                    break;

                case APISearch.SearchComparer.eq:
                    not = term.not ? "<>" : "=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.gt:
                    not = term.not ? "<=" : ">";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.gte:
                    not = term.not ? "<" : ">=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;


                case APISearch.SearchComparer.lt:
                    not = term.not ? ">=" : "<";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;

                case APISearch.SearchComparer.lte:
                    not = term.not ? ">" : "<=";
                    filter += $" ({term.field} {not} {term.values[0]}) ";
                    break;



            }

            return filter;
        }

        private string GetTimeFilterAsString(APISearchTerm term)
        {
            string filter = "";
            string not = "";
            DateTime d1 = DateTime.UtcNow;
            DateTime d2 = DateTime.UtcNow;

            if (term.values.Count == 2)
            {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                d2 = DateTime.Parse(term.values[1], null, System.Globalization.DateTimeStyles.RoundtripKind);
                d1 = new DateTime(2001, 1, 1, d1.Hour, d1.Minute, d1.Second);
                d2 = new DateTime(2001, 1, 1, d1.Hour, d1.Minute, d1.Second);
            }
            else {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                d1 = new DateTime(2001, 1, 1, d1.Hour, d1.Minute, d1.Second);
            }

                switch (term.op)
            {

                case APISearch.SearchComparer.between:
                    if (term.values.Count == 2)
                    {
                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} between '{d1.ToString("yyyy/MM/dd HH:mm:ss")}' and  '{d2.ToString("yyyy/MM/dd HH:mm:ss")}' ) ";
                    }
                    break;
 
                case APISearch.SearchComparer.eq:
           
                        not = term.not ? "<>" : "=";
                        filter += $" ({term.field} {not} '{d1.ToString("yyyy/MM/dd HH:mm:ss")}') ";
         
                    break;

                case APISearch.SearchComparer.gt:
                   
                        not = term.not ? "<=" : ">";
                        filter += $" ({term.field} {not} '{d1.ToString("yyyy/MM/dd HH:mm:ss")}') ";
               
                    break;

                case APISearch.SearchComparer.gte:

                    not = term.not ? "<" : ">=";
                    filter += $" ({term.field} {not} '{d1.ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                    


                case APISearch.SearchComparer.lt:

                    not = term.not ? ">=" : "<";
                    filter += $" ({term.field} {not} '{d1.ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.lte:
                    not = term.not ? ">" : "<=";
                    filter += $" ({term.field} {not} '{d1.ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;
 

            }

            return filter;
        }

        private string GetDatetimeFilterAsString(APISearchTerm term, int offset, bool datetimeasdate)
        {
            string filter = "";
            string not = "";
            DateTime d1 = DateTime.UtcNow;
            DateTime d2 = DateTime.UtcNow;
            
            if (term.values.Count == 2)
            {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                d2 = DateTime.Parse(term.values[1], null, System.Globalization.DateTimeStyles.RoundtripKind);
                
            }
            else
            {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                
            }

            if (datetimeasdate) {
                d1 = new DateTime(d1.Year, d1.Month, d1.Day, 0, 0, 0);
                d2 = new DateTime(d2.Year, d1.Month, d1.Day, 23, 59, 59);
            }

            DateTimeOffset do1 = new DateTimeOffset(d1, new TimeSpan(offset, 0, 0));
            DateTimeOffset do2 = new DateTimeOffset(d2, new TimeSpan(offset, 0, 0));


            switch (term.op)
            {

                case APISearch.SearchComparer.between:
                    if (term.values.Count == 2)
                    {
                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} between '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}' and  '{do2.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}' ) ";
                    }
                    break;

                case APISearch.SearchComparer.eq:

                    not = term.not ? "<>" : "=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.gt:

                    not = term.not ? "<=" : ">";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.gte:

                    not = term.not ? "<" : ">=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;




                case APISearch.SearchComparer.lt:

                    not = term.not ? ">=" : "<";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.lte:
                    not = term.not ? ">" : "<=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;


            }

            return filter;
        }

        private string GetDateFilterAsString(APISearchTerm term, int offset) {
            string filter = "";
            string not = "";
            DateTime d1 = DateTime.UtcNow;
            DateTime d2 = DateTime.UtcNow;

            if (term.values.Count == 2)
            {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);
                d2 = DateTime.Parse(term.values[1], null, System.Globalization.DateTimeStyles.RoundtripKind);

            }
            else
            {
                d1 = DateTime.Parse(term.values[0], null, System.Globalization.DateTimeStyles.RoundtripKind);

            }

 
            DateTimeOffset do1 = new DateTimeOffset(d1, new TimeSpan(offset, 0, 0));
            DateTimeOffset do2 = new DateTimeOffset(d2, new TimeSpan(offset, 0, 0));


            switch (term.op)
            {

                case APISearch.SearchComparer.between:
                    if (term.values.Count == 2)
                    {
                        not = term.not ? "not" : "";
                        filter += $" ({term.field} {not} between '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}' and  '{do2.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}' ) ";
                    }
                    break;

                case APISearch.SearchComparer.eq:

                    not = term.not ? "<>" : "=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.gt:

                    not = term.not ? "<=" : ">";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.gte:

                    not = term.not ? "<" : ">=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;




                case APISearch.SearchComparer.lt:

                    not = term.not ? ">=" : "<";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;

                case APISearch.SearchComparer.lte:
                    not = term.not ? ">" : "<=";
                    filter += $" ({term.field} {not} '{do1.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss")}') ";

                    break;


            }

            return filter;
        }

  
    }
}
