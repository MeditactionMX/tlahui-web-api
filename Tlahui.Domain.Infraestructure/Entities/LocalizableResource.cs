using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base;

namespace Tlahui.Domain.Infraestructure.Entities
{

    
    [Table("LocalizableResource", Schema = "Infrastructure")]
    public class LocalizableResource
    {



        /// <summary>
        /// Resource FQN, ie assembly.namespace.class.property, module.form.fieldname etc.
        /// </summary>
        [Key, Column(Order =1)]
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



        [Key, Column(Order = 3)]
        [MaxLength(5)]
        public string Language { get; set; }

        /// <summary>
        /// Culture as local especific for language
        /// </summary>
        [Key, Column(Order = 4)]
        [MaxLength(5)]
        public string Culture { get; set; }


        [Required(AllowEmptyStrings = true)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string ShortId { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string TraslationId { get; set; }

        /// <summary>
        /// Resource traslation
        /// </summary>
        [Required(AllowEmptyStrings =true)]
        public string Traslation { get; set; }

        /// <summary>
        /// Traslation context to help human translators to provide a more appropiate traslation
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string Context { get; set; }


        [Required(AllowEmptyStrings = true)]
        public string Plural { get; set; }

    }
}
