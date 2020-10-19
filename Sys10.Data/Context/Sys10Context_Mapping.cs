using Sys10.Data.Models.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace Sys10.Data.Context
{
    public partial class Sys10Context
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Sys10Context>(null);
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
