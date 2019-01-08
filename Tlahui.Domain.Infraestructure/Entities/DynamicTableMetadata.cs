using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base;
using Tlahui.Domain.Shared;


namespace Tlahui.Domain.Infraestructure.Entities
{

    [Table("DynamicTableMetadata", Schema = "Infrastructure")]
    public class DynamicTableMetadata
    {


        /// <summary>
        /// Resource FQN, ie assembly.namespace.class.property, module.form.fieldname etc.
        /// </summary>
        [Key, Column(Order = 1)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string ResourceGroupId { get; set; }


        /// <summary>
        /// Language according to locale definition
        /// </summary/// <summary>
        /// Resource FQN, ie assembly.namespace.class.property, module.form.fieldname etc.
        /// </summary>
        [Key, Column(Order = 2)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string ResourceId { get; set; }


        [Required(AllowEmptyStrings = true)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string ShortId { get; set; }


        /// <summary>
        /// True to enable default column display
        /// </summary>
        public bool DisplayByDefault { get; set; } 

        /// <summary>
        /// True if column is searchable
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Default position for column within table
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// Base type for column data
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Format to apply on string convertion
        /// </summary>
        public string OutpuFormat { get; set; }

        /// <summary>
        /// True if culumn is an ID
        /// </summary>
        public bool IsID { get; set; }

        /// <summary>
        /// Indicates tokeep column hidden on UI
        /// </summary>
        public bool AlwaysHidden { get; set; }


        /// <summary>
        /// Determines if colum is sort default
        /// </summary>
        public bool DeafultSort { get; set; }

        /// <summary>
        /// Enable to set a column as dicttionary key 
        /// </summary>
        public bool DictionaryKey { get; set; }

        /// <summary>
        /// Enable to set a column as dicttionary value 
        /// </summary>
        public bool DictionaryValue { get; set; }

        /// <summary>
        /// Field position on returned dictionary value
        /// </summary>
        public int DictionaryValueIndex { get; set; }

        /// <summary>
        /// Set an endpoint to collect a dictonary of entities
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string APIDictionaryEndpoint { get; set; }

        /// <summary>
        /// Determines display type for boolean values
        /// </summary>
        public BooleanDisplayType BoolDisplayType { get; set; }

        /// <summary>
        /// Specifies if a field is a logical deletion mark
        /// </summary>
        public bool MarkDeletedField { get; set; }
    }
}
