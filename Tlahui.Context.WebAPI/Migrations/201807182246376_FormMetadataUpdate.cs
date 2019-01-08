namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormMetadataUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Infrastructure.DynamicFormMetadata", "Type", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "ActionAvailable", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "AddActionAvailable", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "UpdateActionAvailable", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "DeleteActionAvailable", c => c.Boolean(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "DataSourceType", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "ControlType", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "Row", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "Col", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "Width", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Infrastructure.DynamicFormMetadata", "Height", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Infrastructure.DynamicFormMetadata", "DefaultValue", c => c.String(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "MinValue", c => c.String(nullable: false, maxLength: 100));
            AddColumn("Infrastructure.DynamicFormMetadata", "MaxValue", c => c.String(nullable: false, maxLength: 100));
            AddColumn("Infrastructure.DynamicFormMetadata", "MinLen", c => c.Int(nullable: false));
            AddColumn("Infrastructure.DynamicFormMetadata", "MaxLen", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Infrastructure.DynamicFormMetadata", "MaxLen");
            DropColumn("Infrastructure.DynamicFormMetadata", "MinLen");
            DropColumn("Infrastructure.DynamicFormMetadata", "MaxValue");
            DropColumn("Infrastructure.DynamicFormMetadata", "MinValue");
            DropColumn("Infrastructure.DynamicFormMetadata", "DefaultValue");
            DropColumn("Infrastructure.DynamicFormMetadata", "Height");
            DropColumn("Infrastructure.DynamicFormMetadata", "Width");
            DropColumn("Infrastructure.DynamicFormMetadata", "Col");
            DropColumn("Infrastructure.DynamicFormMetadata", "Row");
            DropColumn("Infrastructure.DynamicFormMetadata", "ControlType");
            DropColumn("Infrastructure.DynamicFormMetadata", "DataSourceType");
            DropColumn("Infrastructure.DynamicFormMetadata", "DeleteActionAvailable");
            DropColumn("Infrastructure.DynamicFormMetadata", "UpdateActionAvailable");
            DropColumn("Infrastructure.DynamicFormMetadata", "AddActionAvailable");
            DropColumn("Infrastructure.DynamicFormMetadata", "ActionAvailable");
            DropColumn("Infrastructure.DynamicFormMetadata", "Type");
        }
    }
}
