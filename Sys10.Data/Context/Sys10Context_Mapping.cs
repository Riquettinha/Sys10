using Sys10.Data.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using Sys10.Data.Mapping;

namespace Sys10.Data.Context
{
    public partial class Sys10Context
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Sys10Context>(null);
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new MoovieMap());
            modelBuilder.Configurations.Add(new ArtistMap());
            modelBuilder.Configurations.Add(new GenreMap());
            modelBuilder.Configurations.Add(new CountryMap());
        }
    }
}
