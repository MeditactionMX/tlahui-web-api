namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Tenant.Bucket",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        CreatorId = c.String(maxLength: 50),
                        ModifierId = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.Name)
                .Index(t => t.Deleted);
            
            CreateTable(
                "Store.Category",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        BucketId = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(),
                        KeyWords = c.String(maxLength: 250),
                        DisplayOrder = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        ParentCategoryId = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Tenant.Bucket", t => t.BucketId)
                .ForeignKey("Store.Category", t => t.ParentCategoryId)
                .Index(t => t.BucketId)
                .Index(t => t.ParentCategoryId)
                .Index(t => t.Deleted);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Store.Category", "ParentCategoryId", "Store.Category");
            DropForeignKey("Store.Category", "BucketId", "Tenant.Bucket");
            DropIndex("Store.Category", new[] { "Deleted" });
            DropIndex("Store.Category", new[] { "ParentCategoryId" });
            DropIndex("Store.Category", new[] { "BucketId" });
            DropIndex("Tenant.Bucket", new[] { "Deleted" });
            DropIndex("Tenant.Bucket", new[] { "Name" });
            DropIndex("Tenant.Bucket", new[] { "UserId" });
            DropTable("Store.Category");
            DropTable("Tenant.Bucket");
        }
    }
}
