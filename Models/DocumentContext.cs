using System.Data.Entity;

namespace DocumentRegistration.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DocumentContext : DbContext
    {

        public DbSet<DocumentModel> Document { get; set; }
    }
}