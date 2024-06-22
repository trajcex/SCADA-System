namespace CoreService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AlarmValues");
        }
    }
}
