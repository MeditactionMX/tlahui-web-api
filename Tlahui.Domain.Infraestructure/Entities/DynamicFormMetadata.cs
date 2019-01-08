using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base;
using Tlahui.Domain.Shared;

namespace Tlahui.Domain.Infraestructure.Entities
{

    [Table("DynamicFormMetadata", Schema = "Infrastructure")]
    public class DynamicFormMetadata
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

        [Required]
        /// <summary>
        /// Base type for column data
        /// </summary>
        public Shared.DataType Type { get; set; }

        /// <summary>
        /// Enable to set a column as dicttionary key 
        /// </summary>
        /// 
        [Required]
        public bool DictionaryKey { get; set; }

        /// <summary>
        /// Enable to set a column as dicttionary value 
        /// </summary>
        /// 
        [Required]
        public bool DictionaryValue { get; set; }

        /// <summary>
        /// Field position on returned dictionary value
        /// </summary>
        /// 
        [Required]
        [DefaultValue(0)]
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
        /// Dtermines if element partitipates on CRUD action form
        /// </summary>
        /// 
        [Required]
        public bool ActionAvailable { get; set; }

        [Required]
        public bool AddActionAvailable { get; set; }

        [Required]
        public bool UpdateActionAvailable { get; set; }

        [Required]
        public bool DeleteActionAvailable { get; set; }

        /// <summary>
        /// Set soruce input for form data
        /// </summary>

        [Required]
        public DataSourceType DataSourceType { get; set; }

        /// <summary>
        /// Set display type for form data
        /// </summary>
        /// 
        [Required]
        public ControlType ControlType { get; set; }


        /// <summary>
        /// Row to display form control
        /// </summary>
        /// 
        [Required]
        [DefaultValue(0)]
        public int Row { get; set; }

        /// <summary>
        /// Column to display form control
        /// </summary>
        /// 
        [Required]
        [DefaultValue(0)]
        public int Col { get; set; }


        /// <summary>
        /// Control's width
        /// </summary>
        /// 
        [Required(AllowEmptyStrings =true)]
        [DefaultValue("")]
        [MaxLength(50)]
        public string Width { get; set; }

        /// <summary>
        /// Control's height
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        [MaxLength(50)]
        public string Height { get; set; }

        /// <summary>
        /// Default value for field
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        public string DefaultValue { get; set; }


        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        [MaxLength(100)]
        public string MinValue { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        [MaxLength(100)]
        public string MaxValue { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DefaultValue(0)]
        public int MinLen { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DefaultValue(0)]
        public int MaxLen { get; set; }


        [Required]
        [DefaultValue(false)]
        public bool Required { get; set; }

    }
}
