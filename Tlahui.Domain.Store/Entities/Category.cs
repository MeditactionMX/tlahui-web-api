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
using Tlahui.Domain.Shared;

namespace Tlahui.Domain.Store.Entities
{

    /// <summary>
    /// Agrupador temático para los objetos (productos/servicios/sucripciones, etc.) 
    /// de una tienda.
    /// </summary>
    [Table("Category", Schema = "Store")]
    [Localizable("Categoría", "Categorías", Context = "")]
    [Localizable("Category", "Categories", Context = "", Culture = "US", Language = "en")]
    public class Category : GUIDEntity, ITrackableEntity, IBucketTrackable, ILocalizableResource, IDynamicUIResource
    {

        //Especifica el query para la consulta 
        public const string QUERY = @"select c.id, c.BucketId, c.Name, c.Description, c.KeyWords, c.DisplayOrder , c.Published , c.ParentCategoryId, 
                        c.Deleted , c.CreateDate, c.CreatorId, c.UpdateDate, c.ModifierId , c.DeletedDate , c.DeleterId ,
                        pc.Name as ParentCategoryLabel, '' as CreatorLabel, '' as ModifierLabel, '' as DeleterLabel
                        from store.Category c
                        left join store.Category pc on c.ParentCategoryId = pc.Id";

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
        [EntitiesUIMetadata (AlwaysHidden =true, DisplayByDefault =false,  Searchable =false, Type = Shared.DataType.text,
             DataSourceType = Shared.DataSourceType.server_session, Required =true )]
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
        [EntitiesUIMetadata (DisplayIndex = 1, DefaultSort =true, DictionaryValue =true, DictionaryValueIndex =0, Type = Shared.DataType.text,
            DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.inputbox, ActionAvailable =true, AddActionAvailable =true, UpdateActionAvailable =true, Col =1, Row=1, Width ="100%", Required = true)]
        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string Name { get; set; }



        [Localizable("Descripción", "Descripciones", Context = "")]
        [Localizable("Description", "Descriptions", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (DisplayIndex = 2, Searchable = true, Type = Shared.DataType.text,
            DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.text_area, ActionAvailable = true, AddActionAvailable = true, UpdateActionAvailable = true, Col = 1, Row = 2, Width = "100%", Height ="200px", Required = true)]
        /// <summary>
        /// Descripción de la cartgoría
        /// </summary>
        public string Description { get; set; }



        [Localizable("Palabras clave", "Palabras clave", Context = "")]
        [Localizable("Keywords", "Keywords", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 3, Searchable = true, Type = Shared.DataType.text,
            DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.text_area, ActionAvailable = true, AddActionAvailable = true, UpdateActionAvailable = true, Col = 1, Row = 3, Width = "100%", Height = "200px", Required = true)]
        [MaxLength(CodeFirstConstats.KEYWORDS_FIELD_LEN)]
        /// <summary>
        /// Palabras clave asociadas a la caetgprí
        /// </summary>
        public string KeyWords { get; set; }


        [Required]
        [Localizable("Orden despliegue", "Orden despliegue", Context = "")]
        [Localizable("Display order", "Display order", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 4, Type = Shared.DataType.integer_number,
            DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.inputbox, ActionAvailable = true, AddActionAvailable = true, UpdateActionAvailable = true, Col = 1, Row = 5, Width = "50%", DefaultValue ="0")]
        /// <summary>
        /// Ordén de despliegue para la categoria
        /// </summary>
        public int DisplayOrder { get; set; }


        [Required]
        [Localizable("Publicada", "Publicadas", Context = "")]
        [Localizable("Published", "Published", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 5, Type = Shared.DataType.boolean,
              DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.checkbox, ActionAvailable = true, AddActionAvailable = true, UpdateActionAvailable = true, Col = 2, Row = 5, Width = "50%", Required = true)]
        /// <summary>
        /// Espeifica si la categoria está disponible públicamente
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Enlace a las categorías hijas de la catgoría actual
        /// </summary>
        [JsonIgnore]
        public virtual IQueryable<Category> Categories { get; set; }


        [Localizable("Categoría padre", "Categorías padre", Context = "")]
        [Localizable("Parent category", "Parent categories", Context = "", Culture = "US", Language = "en")]
        [EntitiesUIMetadata(DisplayByDefault = false, Searchable = false, Type = Shared.DataType.text,
         APIDictionaryEndpoint = "/api/store/categories/", 
         DataSourceType = Shared.DataSourceType.user_input, ControlType = ControlType.select_localapi_elements, 
         ActionAvailable = true, AddActionAvailable = true, UpdateActionAvailable = true, Col = 2, Row = 5, Width = "50%", DefaultValue ="0", Required =true)]
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
        [EntitiesUIMetadata (DisplayByDefault = false, DisplayIndex = 6, Searchable = true, Type = Shared.DataType.text,
             APIDictionaryEndpoint = "/api/store/categories/")]
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
