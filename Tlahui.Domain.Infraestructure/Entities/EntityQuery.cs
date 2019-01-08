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

    [Table("EntityQuery", Schema = "Infrastructure")]
    public class EntityQuery
    {

        [Key, Column(Order = 1)]
        [MaxLength(CodeFirstConstats.GENERAL_FIELDID_LEN)]
        public string ResourceId { get; set; }

        [Required]
        public string Query { get; set; } 

    }
}
