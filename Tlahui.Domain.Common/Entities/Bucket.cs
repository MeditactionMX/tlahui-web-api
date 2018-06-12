using CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base;
using Tlahui.Domain.Base.Entities;

namespace Tlahui.Domain.Common.Tenant
{
    /// <summary>
    /// A bucket is an administrative entitiy that is used to group resources 
    /// associated with an application account
    /// </summary>
    /// 
    [Table("Bucket", Schema = "Tenant")]
    public class Bucket : GUIDEntity, ITrackableEntity, ILocalizableResource
    {
        public Bucket():base()
        {
 
        }

        /// <summary>
        /// UserId associated to bucket
        /// </summary>
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        [Index]
        [Required]
        public string UserId { get; set; }
 
      

        /// <summary>
        /// A human representative name for the bucket
        /// </summary>
        [Localizable("Nombre del Bucket", "Nombre del Bucket", Context = "")]
        [Localizable("Bucket Name", "Bucket Name", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 1, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [MaxLength(250)]
        [Required]
        [Index]
        public string Name { get; set; }


        /// <summary>
        /// Permite mostrar el nombre del dueño o creador de un Bucket
        /// </summary>
        [NotMapped]
        [Localizable("Propietario", "Propietarios", Context = "")]
        [Localizable("Owner", "Owners", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 2, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        public string UserFullNameLabel { get; set; }


        #region TrackableEntityProps
        public DateTime CreateDate { get; set; }

        public string CreatorId { get; set; }

        [NotMapped]
        public string CreatorLabel { get; set; }


        public DateTime UpdateDate { get; set; }

        public string ModifierId { get; set; }

        [NotMapped]
        public string ModifierLabel { get; set; }


        public bool Deleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeleterId { get; set; }

        [NotMapped]
        public string DeleterLabel { get; set; }


        #endregion


    }
}
