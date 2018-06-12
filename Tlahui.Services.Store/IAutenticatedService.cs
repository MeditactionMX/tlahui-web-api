using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Services.Store
{
    public interface IAutenticatedService
    {


        string BucketId { get; set; }

        string UserId { get; set; }

    }
}
