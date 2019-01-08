namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DynamicFormsMetadata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Infrastructure.DynamicFormMetadata",
                c => new
                    {
                        ResourceGroupId = c.String(nullable: false, maxLength: 250),
                        ResourceId = c.String(nullable: false, maxLength: 250),
                        ShortId = c.String(nullable: false, maxLength: 250),
                        DictionaryKey = c.Boolean(nullable: false),
                        DictionaryValue = c.Boolean(nullable: false),
                        DictionaryValueIndex = c.Int(nullable: false),
                        APIDictionaryEndpoint = c.String(nullable: false),
                        BoolDisplayType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceGroupId, t.ResourceId });
            
        }
        
        public override void Down()
        {
            DropTable("Infrastructure.DynamicFormMetadata");
        }
    }
}
