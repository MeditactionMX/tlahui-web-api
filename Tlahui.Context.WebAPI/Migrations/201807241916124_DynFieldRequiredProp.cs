namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DynFieldRequiredProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("Infrastructure.DynamicFormMetadata", "Required", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Infrastructure.DynamicFormMetadata", "Required");
        }
    }
}
