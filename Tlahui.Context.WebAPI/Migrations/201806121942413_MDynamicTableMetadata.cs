namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MDynamicTableMetadata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Infrastructure.DynamicTableMetadata",
                c => new
                    {
                        ResourceGroupId = c.String(nullable: false, maxLength: 250),
                        ResourceId = c.String(nullable: false, maxLength: 250),
                        ShortId = c.String(nullable: false, maxLength: 250),
                        DisplayByDefault = c.Boolean(nullable: false),
                        Searchable = c.Boolean(nullable: false),
                        DisplayIndex = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        OutpuFormat = c.String(),
                        IsID = c.Boolean(nullable: false),
                        AlwaysHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceGroupId, t.ResourceId });
            
        }
        
        public override void Down()
        {
            DropTable("Infrastructure.DynamicTableMetadata");
        }
    }
}
