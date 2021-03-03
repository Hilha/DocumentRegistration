﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DocumentRegistration.Models;
using MySql.Data.EntityFramework;

namespace DocumentRegistration.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DocumentContext : DbContext
    {
        public DocumentContext () : base("name=MySqlDb")
        {

        }

        public virtual DbSet<DocumentModel> DocumentModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
        }

    }
}