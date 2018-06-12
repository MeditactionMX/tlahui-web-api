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

    [Table("CachedResourceStatistics", Schema = "Infrastructure")]
    public class CachedResourceStatistics
    {
        [Key, Column(Order = 1)]
        public string ResourceKey { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(CodeFirstConstats.GUID_ID_LEN)]
        public string BucketId { get; set; }

        public DateTime LastUpdated { get; set; }

        public int Count { get; set; }

    }
}
