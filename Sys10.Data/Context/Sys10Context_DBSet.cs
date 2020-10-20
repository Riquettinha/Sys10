using Sys10.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Context
{
    public partial class Sys10Context
    {
        public DbSet<User> User { get; set; }
        public DbSet<Moovie> Moovie { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Country> Country { get; set; }
    }
}
