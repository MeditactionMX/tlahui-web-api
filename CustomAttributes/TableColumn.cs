using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{

    [System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class TableColumn : System.Attribute
    {

        public enum DataType {
            text = 1, integer_number = 10, decimal_number=11, date = 20, time = 21, datetime = 22, boolean = 3
        }

        /// <summary>
        /// True to enable default column display
        /// </summary>
        public bool DisplayByDefault;

        /// <summary>
        /// True if column is searchable
        /// </summary>
        public bool Searchable;

        /// <summary>
        /// Default position for column within table
        /// </summary>
        public int DisplayIndex;

        /// <summary>
        /// Base type for column data
        /// </summary>
        public DataType Type;

        /// <summary>
        /// Format to apply on string convertion
        /// </summary>
        public string OutpuFormat;

        /// <summary>
        /// True if culumn is an ID
        /// </summary>
        public bool IsID;

        /// <summary>
        /// Indicates tokeep column hidden on UI
        /// </summary>
        public bool AlwaysHidden;


        public TableColumn() {
            this.DisplayByDefault = true;
            this.Searchable = true;
            this.DisplayIndex = 0;
            this.Type = DataType.text;
            this.OutpuFormat = "";
            this.IsID = false;
            this.AlwaysHidden = false;
        }

    }
}
