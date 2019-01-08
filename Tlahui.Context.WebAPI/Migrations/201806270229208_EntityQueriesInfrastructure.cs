namespace Tlahui.Context.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityQueriesInfrastructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Infrastructure.EntityQuery",
                c => new
                    {
                        ResourceId = c.String(nullable: false, maxLength: 250),
                        Query = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ResourceId);
            
        }
        
        public override void Down()
        {
            DropTable("Infrastructure.EntityQuery");
        }
    }
}
