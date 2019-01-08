using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Shared;

namespace CustomAttributes
{


    [System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class EntitiesUIMetadata : System.Attribute
    {
 
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


        /// <summary>
        /// Determines if colum is sort default
        /// </summary>
        public bool DefaultSort;


        /// <summary>
        /// Enable to set a column as dicttionary key 
        /// </summary>
        public bool DictionaryKey;


        /// <summary>
        /// Enable to set a column as dicttionary value 
        /// </summary>
        public bool DictionaryValue;

        /// <summary>
        /// Field position on returned dictionary value
        /// </summary>
        public int DictionaryValueIndex;


        /// <summary>
        /// Set an endpoint to collect a dictonary of entities
        /// </summary>
        public string APIDictionaryEndpoint;

        /// <summary>
        /// Determines display type for boolean values
        /// </summary>
        public BooleanDisplayType BoolDisplayType;

        /// <summary>
        /// Specifies if a field is a logical deletion mark
        /// </summary>
        public bool MarkDeletedField;


        #region Form Control


        /// <summary>
        /// Dtermines if element partitipates on CRUD action form
        /// </summary>
        public bool ActionAvailable;
        public bool AddActionAvailable;
        public bool UpdateActionAvailable;
        public bool DeleteActionAvailable;

        /// <summary>
        /// Set soruce input for form data
        /// </summary>
        public DataSourceType DataSourceType;

        /// <summary>
        /// Set display type for form data
        /// </summary>
        public ControlType ControlType;


        /// <summary>
        /// Row to display form control
        /// </summary>
        public int Row;

        /// <summary>
        /// Column to display form control
        /// </summary>
        public int Col;


        /// <summary>
        /// Control's width
        /// </summary>
        public string Width;

        /// <summary>
        /// Control's height
        /// </summary>
        public string Height;

        /// <summary>
        /// Default value for field
        /// </summary>
        public string DefaultValue;

        /// <summary>
        /// Set min len for text props
        /// </summary>
        public int MinLen;

        /// <summary>
        /// Set max len for text props
        /// </summary>
        public int MaxLen;

        /// <summary>
        /// Holds min value for range based props
        /// </summary>
        public string MinValue;


        /// <summary>
        /// Holds max value for range based props
        /// </summary>
        public string MaxValue;

        /// <summary>
        /// Determines if field is required
        /// </summary>
        public bool Required;

        #endregion


        public EntitiesUIMetadata() {
            this.MinValue = "";
            this.MaxValue = "";
            this.MinLen = 0;
            this.MaxLen = 0;
            this.DisplayByDefault = true;
            this.Searchable = true;
            this.DisplayIndex = int.MaxValue;
            this.Type = DataType.text;
            this.OutpuFormat = "";
            this.IsID = false;
            this.AlwaysHidden = false;
            this.DefaultSort = false;
            this.DictionaryKey = false;
            this.DictionaryValue = false;
            this.APIDictionaryEndpoint = "";
            this.DictionaryValueIndex = 0;
            this.BoolDisplayType = BooleanDisplayType.none;
            this.MarkDeletedField = false;
            this.ControlType = ControlType.inputbox;
            this.Row = 0;
            this.Col = 0;
            this.Width = "";
            this.Height = "";
            this.ActionAvailable = false;
            this.UpdateActionAvailable = false;
            this.AddActionAvailable = false;
            this.DeleteActionAvailable = false;
            this.DataSourceType = DataSourceType.none;
            this.DefaultValue = "";
            this.Required = false;
        }

    }
}
