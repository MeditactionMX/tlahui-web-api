using CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Enumerations;

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
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1000, Type = Shared.DataType.datetime)]
        [Localizable("Fecha de creación", "Fechas de creación", Context = "")]
        [Localizable("Created on", "Created on", Context = "", Culture = "US", Language = "en")]
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Creator's ID
        /// </summary>
        [EntitiesUIMetadata (AlwaysHidden = true, DisplayByDefault = false, Searchable = false, Type = Shared.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string CreatorId { get; set; }


        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1001, Type = Shared.DataType.text, APIDictionaryEndpoint = "api/tenant/users/")]
        [Localizable("Creado por", "Creado por", Context = "")]
        [Localizable("Created by", "Created by", Context = "", Culture = "US", Language = "en")]
        [NotMapped]
        string CreatorLabel { get; set; }


        /// <summary>
        /// Last UTC modification date
        /// </summary>
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1010, Searchable = true, Type = Shared.DataType.datetime)]
        [Localizable("Fecha de actualización", "Fechas de actualización", Context = "")]
        [Localizable("Updated on", "Updated on", Context = "", Culture = "US", Language = "en")]
        DateTime UpdateDate { get; set; }



        /// <summary>
        /// Midifier's Id
        /// </summary>
        [EntitiesUIMetadata (AlwaysHidden = true, DisplayByDefault = false, Searchable = false, Type = Shared.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string ModifierId { get; set; }



        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1011, Type = Shared.DataType.text, APIDictionaryEndpoint = "api/tenant/users/")]
        [Localizable("Eliminado por", "Eliminados por", Context = "")]
        [Localizable("Deleted by", "Deleted by", Context = "", Culture = "US", Language = "en")]
        [NotMapped]
        string ModifierLabel { get; set; }



        [EntitiesUIMetadata (MarkDeletedField =true, DisplayByDefault = false, DisplayIndex = 1020, Type = Shared.DataType.boolean, BoolDisplayType = Shared.BooleanDisplayType.YesNo)]
        [Localizable("Eliminado", "Eliminados", Context = "")]
        [Localizable("Deleted", "Deleted", Context = "", Culture = "US", Language = "en")]
        bool Deleted { get; set; }



        [Localizable("Fecha de eliminación", "Fechas de eliminación", Context = "")]
        [Localizable("Deleted on", "Deleted on", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1021, Type = Shared.DataType.datetime)]
        DateTime? DeletedDate { get; set; }

 
        [EntitiesUIMetadata (AlwaysHidden = true, DisplayByDefault = false, Searchable = false, Type = Shared.DataType.text)]
        [MaxLength(CodeFirstConstats.USER_ID_LEN)]
        string DeleterId { get; set; }
 

        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 1022, Type = Shared.DataType.text, APIDictionaryEndpoint = "api/tenant/users/")]
        [Localizable("Eliminado por", "Eliminados por", Context = "")]
        [Localizable("Deleted by", "Deleted by", Context = "", Culture = "US", Language = "en")]
        string DeleterLabel { get; set; }

    }
}
