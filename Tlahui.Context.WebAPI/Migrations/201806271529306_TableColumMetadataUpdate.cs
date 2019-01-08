namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableColumMetadataUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Infrastructure.DynamicTableMetadata", "DeafultSort", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicTableMetadata", "DictionaryKey", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicTableMetadata", "DictionaryValue", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicTableMetadata", "DictionaryValueIndex", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicTableMetadata", "APIDictionaryEndpoint", c => c.String(nullable: false, defaultValue: ""));
        }
        
        public override void Down()
        {
            DropColumn("Infrastructure.DynamicTableMetadata", "APIDictionaryEndpoint");
            DropColumn("Infrastructure.DynamicTableMetadata", "DictionaryValueIndex");
            DropColumn("Infrastructure.DynamicTableMetadata", "DictionaryValue");
            DropColumn("Infrastructure.DynamicTableMetadata", "DictionaryKey");
            DropColumn("Infrastructure.DynamicTableMetadata", "DeafultSort");
        }
    }
}
