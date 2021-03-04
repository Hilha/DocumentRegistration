namespace DocumentRegistration.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DocumentRegistration.DAL.DocumentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DocumentRegistration.DAL.DocumentContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
