using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pentagon.Models
{
    public class TutorialContext:DbContext
    {
        public DbSet<Tutorial> Tutorials { get; set; }
    }
}