using CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Base.Entities
{
    public interface IBucketTrackable
    {
        /// <summary>
        /// Identificador del bucket al que pertence el objeto
        /// </summary>
        /// 
        string BucketId { get; set; }
    }
}
