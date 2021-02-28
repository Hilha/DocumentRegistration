using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DocumentRegistration.Models
{
    public class DocumentContext : DbContext
    {

        public DbSet<DocumentModel> Document { get; set; }
    }
}