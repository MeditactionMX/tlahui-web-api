namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CacheAndLocalization : DbMigration
    {
        public override void Up()
        {
            DropIndex("Tenant.Bucket", new[] { "UserId" });
            CreateTable(
                "Infrastructure.CachedResourceStatistics",
                c => new
                    {
                        ResourceKey = c.String(nullable: false, maxLength: 128),
                        BucketId = c.String(nullable: false, maxLength: 50),
                        LastUpdated = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceKey, t.BucketId });
            
            CreateTable(
                "Infrastructure.LocalizableResource",
                c => new
                    {
                        ResourceId = c.String(nullable: false, maxLength: 250),
                        Language = c.String(nullable: false, maxLength: 5),
                        Culture = c.String(nullable: false, maxLength: 5),
                        ShortId = c.String(nullable: false, maxLength: 250),
                        TraslationId = c.String(nullable: false, maxLength: 250),
                        Traslation = c.String(nullable: false),
                        Context = c.String(nullable: false),
                        Plural = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceId, t.Language, t.Culture });
            
            AddColumn("Tenant.Bucket", "DeletedDate", c => c.DateTime());
            AddColumn("Tenant.Bucket", "DeleterId", c => c.String(maxLength: 128));
            AddColumn("Store.Category", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("Store.Category", "CreatorId", c => c.String());
            AddColumn("Store.Category", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("Store.Category", "ModifierId", c => c.String());
            AddColumn("Store.Category", "CreatorLabel", c => c.String());
            AddColumn("Store.Category", "ModifierLabel", c => c.String());
            AddColumn("Store.Category", "DeletedDate", c => c.DateTime());
            AddColumn("Store.Category", "DeleterId", c => c.String(maxLength: 128));
            AlterColumn("Tenant.Bucket", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Tenant.Bucket", "CreatorId", c => c.String(maxLength: 128));
            AlterColumn("Tenant.Bucket", "ModifierId", c => c.String(maxLength: 128));
            CreateIndex("Tenant.Bucket", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("Tenant.Bucket", new[] { "UserId" });
            AlterColumn("Tenant.Bucket", "ModifierId", c => c.String(maxLength: 50));
            AlterColumn("Tenant.Bucket", "CreatorId", c => c.String(maxLength: 50));
            AlterColumn("Tenant.Bucket", "UserId", c => c.String(nullable: false, maxLength: 50));
            DropColumn("Store.Category", "DeleterId");
            DropColumn("Store.Category", "DeletedDate");
            DropColumn("Store.Category", "ModifierLabel");
            DropColumn("Store.Category", "CreatorLabel");
            DropColumn("Store.Category", "ModifierId");
            DropColumn("Store.Category", "UpdateDate");
            DropColumn("Store.Category", "CreatorId");
            DropColumn("Store.Category", "CreateDate");
            DropColumn("Tenant.Bucket", "DeleterId");
            DropColumn("Tenant.Bucket", "DeletedDate");
            DropTable("Infrastructure.LocalizableResource");
            DropTable("Infrastructure.CachedResourceStatistics");
            CreateIndex("Tenant.Bucket", "UserId");
        }
    }
}
