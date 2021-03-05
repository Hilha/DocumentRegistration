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
                    Code = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                    Title = c.String(nullable: false, unicode: false),
                    Category = c.String(nullable: false, unicode: false),
                    Process = c.String(nullable: false, unicode: false),
                    FilePath = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);
                Sql("CREATE index `IX_Code` on `Document` (`Code` DESC)");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Document", new[] { "Code" });
            DropTable("dbo.Document");
        }
    }
}
