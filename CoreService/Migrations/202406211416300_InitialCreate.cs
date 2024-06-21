namespace CoreService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmId = c.String(unicode: false),
                        Type = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(unicode: false),
                        Type = c.String(unicode: false),
                        Time = c.String(unicode: false),
                        TagName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Password = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.TagValues");
            DropTable("dbo.AlarmValues");
        }
    }
}
