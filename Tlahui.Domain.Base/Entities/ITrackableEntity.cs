using CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Domain.Base.Entities
{

    /// <summary>
    /// Esta interfz proporciona la infraectructura para realizar el segumiento
    /// a los cambios realizados en un objeto
    /// </summary>
    public interface ITrackableEntity
    {

        /// <summary>
        /// UTC Creation Date
        /// </summary>
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.datetime)]
        [Localizable("Fecha de creación", "Fechas de creación", Context = "")]
        [Localizable("Created on", "Created on", Context = "", Culture = "US", Language = "en")]
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Creator's ID
        /// </summary>
        [TableColumn(AlwaysHidden = true, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = false, Type = TableColumn.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string CreatorId { get; set; }


        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [Localizable("Creado por", "Creado por", Context = "")]
        [Localizable("Created by", "Created by", Context = "", Culture = "US", Language = "en")]
        [NotMapped]
        string CreatorLabel { get; set; }


        /// <summary>
        /// Last UTC modification date
        /// </summary>
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.datetime)]
        [Localizable("Fecha de actualización", "Fechas de actualización", Context = "")]
        [Localizable("Updated on", "Updated on", Context = "", Culture = "US", Language = "en")]
        DateTime UpdateDate { get; set; }


        
        /// <summary>
        /// Midifier's Id
        /// </summary>
        [TableColumn(AlwaysHidden = true, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = false, Type = TableColumn.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string ModifierId { get; set; }



        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [Localizable("Eliminado por", "Eliminados por", Context = "")]
        [Localizable("Deleted by", "Deleted by", Context = "", Culture = "US", Language = "en")]
        [NotMapped]
        string ModifierLabel { get; set; }



        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.boolean)]
        [Localizable("Eliminado", "Eliminados", Context = "")]
        [Localizable("Deleted", "Deleted", Context = "", Culture = "US", Language = "en")]
        bool Deleted { get; set; }



        [Localizable("Fecha de eliminación", "Fechas de eliminación", Context = "")]
        [Localizable("Deleted on", "Deleted on", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.datetime)]
        DateTime? DeletedDate { get; set; }

 
        [TableColumn(AlwaysHidden = true, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = false, Type = TableColumn.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string DeleterId { get; set; }
 

        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [Localizable("Eliminado por", "Eliminados por", Context = "")]
        [Localizable("Deleted by", "Deleted by", Context = "", Culture = "US", Language = "en")]
        string DeleterLabel { get; set; }

    }
}
