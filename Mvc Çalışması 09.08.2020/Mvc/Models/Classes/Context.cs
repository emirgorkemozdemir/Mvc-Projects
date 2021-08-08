using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc.Models.Classes
{
    public class Context: DbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Cars> Carses { get; set; }
    }
}