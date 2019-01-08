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
    /// Clase de entidad donde el ID es de long autonumeríco
    /// El valor default para una entidad nueva basada en esta clase 
    /// es un GUID en formato texto
    /// </summary>
    public class LongIdEntitiy
    {
        public LongIdEntitiy()
        {
            _Id = 0;
        }

        private long _Id;

        /// <summary>
        /// Identificador único de la entidad
        /// </summary>
        [Localizable("Identificador único", "Identificadores únicos", Context = "")]
        [Localizable("Unique identifier", "Unique identifiers", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 0, IsID = true, OutpuFormat = "", Searchable = true, Type = Shared.DataType.text)]
        [Key]
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        
    }
}
