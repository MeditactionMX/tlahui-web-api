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
    /// Clase de entidad donde el ID es de tipo String
    /// El valor default para una entidad nueva basada en esta clase 
    /// es un GUID en formato texto
    /// </summary>
    public class GUIDEntity
    {

        public GUIDEntity()
        {
            _Id = System.Guid.NewGuid().ToString();
        }

     
        private string _Id;

        /// <summary>
        /// Identificador único de la entidad
        /// </summary>
        [Localizable("Identificador único", "Identificadores únicos", Context = "")]
        [Localizable("Unique identifier", "Unique identifiers", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 0, IsID = true, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [Key]
        [MaxLength(CodeFirstConstats.GUID_ID_LEN)]
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
 

    }
}
