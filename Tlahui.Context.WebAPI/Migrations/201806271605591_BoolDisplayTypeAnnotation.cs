namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolDisplayTypeAnnotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Infrastructure.DynamicTableMetadata", "BoolDisplayType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Infrastructure.DynamicTableMetadata", "BoolDisplayType");
        }
    }
}
