using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Base.Entities;
using Tlahui.Domain.Base;
using Tlahui.Domain.Common.Tenant;
using Newtonsoft.Json;
using CustomAttributes;

namespace Tlahui.Domain.Store.Entities
{

    /// <summary>
    /// Agrupador temático para los objetos (productos/servicios/sucripciones, etc.) 
    /// de una tienda.
    /// </summary>
    [Table("Category", Schema = "Store")]
    [Localizable("Categoría", "Categorías", Context = "")]
    [Localizable("Category", "Categories", Context = "", Culture = "US", Language = "en")]
    public class Category : GUIDEntity, ITrackableEntity, IBucketTrackable, ILocalizableResource
    {

        #region CNST

        public Category() : base()
        {
            this.CreateDate = System.DateTime.UtcNow;
            this.UpdateDate = System.DateTime.UtcNow;
        }

        public Category(string BucketId) : base()
        {
            this._BucketId = BucketId;
            this.CreateDate = System.DateTime.UtcNow;
            this.UpdateDate = System.DateTime.UtcNow;

        }

        #endregion


        private string _BucketId;
        [Index(IsClustered = false)]
        [Localizable("BucketId", "BucketId", Context ="")]
        [Localizable("BucketId", "BucketId", Context = "", Culture ="US", Language ="en")]
        [TableColumn(AlwaysHidden =true, DisplayByDefault =false , DisplayIndex =0, IsID =false, OutpuFormat ="", Searchable =false, Type = TableColumn.DataType.text)]
        /// <summary>
        /// Bucket asociado a la categorías
        /// </summary>
        public string BucketId
        {
            get { return _BucketId; }
            set { _BucketId = value; }
        }

        [Required]
        [MaxLength(CodeFirstConstats.NAME_FIELD_LEN)]
        [Localizable("Categoría","Categorías", Context = "")]
        [Localizable("Category","Categories", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = true, DisplayIndex = 1, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string Name { get; set; }



        [Localizable("Descripción", "Descripciones", Context = "")]
        [Localizable("Description", "Descriptions", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = true, DisplayIndex = 2, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        /// <summary>
        /// Descripción de la cartgoría
        /// </summary>
        public string Description { get; set; }



        [Localizable("Palabras clave", "Palabras clave", Context = "")]
        [Localizable("Keywords", "Keywords", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = int.MaxValue, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        [MaxLength(CodeFirstConstats.KEYWORDS_FIELD_LEN)]
        /// <summary>
        /// Palabras clave asociadas a la caetgprí
        /// </summary>
        public string KeyWords { get; set; }


        [Required]
        [Localizable("Orden despliegue", "Orden despliegue", Context = "")]
        [Localizable("Display order", "Display order", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = true, DisplayByDefault = false, DisplayIndex = 0, IsID = false, OutpuFormat = "", Searchable = false, Type = TableColumn.DataType.integer_number)]
        /// <summary>
        /// Ordén de despliegue para la categoria
        /// </summary>
        public int DisplayOrder { get; set; }


        [Required]
        [Localizable("Publicada", "Publicadas", Context = "")]
        [Localizable("Published", "Published", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 3, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.boolean)]
        /// <summary>
        /// Espeifica si la categoria está disponible públicamente
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Enlace a las categorías hijas de la catgoría actual
        /// </summary>
        [JsonIgnore]
        public virtual IQueryable<Category> Categories { get; set; }

        /// <summary>
        /// Determina el padre de la categoría, si es nulo este campo implica un categoria raiz
        /// </summary>
        public string ParentCategoryId { get; set; }


        /// <summary>
        /// Permite mostrar el nombre de la categoría padre para operaciones de despliegue
        /// </summary>
        [NotMapped]
        [Localizable("Categoría padre", "Categorías padre", Context = "")]
        [Localizable("Parent category", "Parent categories", Context = "", Culture = "US", Language = "en")]
        [TableColumn(AlwaysHidden = false, DisplayByDefault = false, DisplayIndex = 4, IsID = false, OutpuFormat = "", Searchable = true, Type = TableColumn.DataType.text)]
        public string ParentCategoryLabel { get; set; }

        /// <summary>
        /// Categoría padre de la categoría actual
        /// </summary>
        [JsonIgnore]
        public virtual Category ParentCategory { get; set; }

        [JsonIgnore]
        public virtual Bucket Bucket { get; set; }


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
