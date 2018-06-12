namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseEntitesReconfiguration : DbMigration
    {
        public override void Up()
        {
            DropIndex("Tenant.Bucket", new[] { "Deleted" });
            DropIndex("Store.Category", new[] { "Deleted" });
            AlterColumn("Tenant.Bucket", "CreatorId", c => c.String());
            AlterColumn("Tenant.Bucket", "ModifierId", c => c.String());
            AlterColumn("Tenant.Bucket", "DeleterId", c => c.String());
            AlterColumn("Store.Category", "DeleterId", c => c.String());
            DropColumn("Store.Category", "CreatorLabel");
            DropColumn("Store.Category", "ModifierLabel");
        }
        
        public override void Down()
        {
            AddColumn("Store.Category", "ModifierLabel", c => c.String());
            AddColumn("Store.Category", "CreatorLabel", c => c.String());
            AlterColumn("Store.Category", "DeleterId", c => c.String(maxLength: 128));
            AlterColumn("Tenant.Bucket", "DeleterId", c => c.String(maxLength: 128));
            AlterColumn("Tenant.Bucket", "ModifierId", c => c.String(maxLength: 128));
            AlterColumn("Tenant.Bucket", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("Store.Category", "Deleted");
            CreateIndex("Tenant.Bucket", "Deleted");
        }
    }
}
