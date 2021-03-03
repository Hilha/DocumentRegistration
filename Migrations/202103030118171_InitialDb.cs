namespace DocumentRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Title = c.String(nullable: false, unicode: false),
                        Category = c.String(nullable: false, unicode: false),
                        Process = c.String(nullable: false, unicode: false),
                        FilePath = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Document");
        }
    }
}
