using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using TURGWEB.Models;

namespace TURGWEB.Context
{
    public class ContextDb :DbContext
    {
        public ContextDb():base("dbconstring")
        {

        }

        public DbSet<news> DbNews { get; set; }

        public DbSet<Models.types> DbTypes { get; set; }
        public DbSet<Models.parts> DbContents { get; set; }

    }
}