namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalizedresourcesUpdate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Infrastructure.LocalizableResource");
            AddColumn("Infrastructure.LocalizableResource", "ResourceGroupId", c => c.String(nullable: false, maxLength: 250));
            AddPrimaryKey("Infrastructure.LocalizableResource", new[] { "ResourceGroupId", "ResourceId", "Language", "Culture" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("Infrastructure.LocalizableResource");
            DropColumn("Infrastructure.LocalizableResource", "ResourceGroupId");
            AddPrimaryKey("Infrastructure.LocalizableResource", new[] { "ResourceId", "Language", "Culture" });
        }
    }
}
