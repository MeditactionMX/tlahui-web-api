using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tlahui.Domain.Common.Tenant;
using Tlahui.Domain.Infraestructure.Entities;
using Tlahui.Domain.Store.Entities;

namespace Tlahui.Context.WebAPI
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext() : base("tlahui") { }

        public string ConnectionString
        {
            get { return this.Database.Connection.ConnectionString; }
        }


        //DB Sets

        #region Common
        public virtual DbSet<Bucket> Buckets { get; set; }
        #endregion

        #region Store
        public virtual DbSet<Category> Categories { get; set; }
        #endregion

        #region Infrastructure
        public virtual DbSet<CachedResourceStatistics> CachedResourceStatistics { get; set; }
        public virtual DbSet<LocalizableResource> LocalizableResources { get; set; }
        public virtual DbSet<DynamicTableMetadata> DynamicTableMetadataProperties { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Añadir elementos en orden alfabético con el formato // # Char


            // # C
            //=============================================================

            //Relacion padre hijo de objecto Category
            modelBuilder.Entity<Category>().HasOptional(e => e.ParentCategory).
            WithMany().HasForeignKey(x => x.ParentCategoryId);



            //Convenciones
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}