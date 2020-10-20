using Sys10.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Mapping
{
    public class MoovieMap : EntityTypeConfiguration<Moovie>
    {
        public MoovieMap()
        {
            ToTable("Moovie");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(500).IsRequired();
            Property(t => t.ReleaseDate).HasColumnName("ReleaseDate").IsRequired();
            Property(t => t.DirectorId).HasColumnName("DirectorId").IsRequired();
            Property(t => t.CountryId).HasColumnName("CountryId").IsRequired();
            Property(t => t.GenreId).HasColumnName("GenreId").IsRequired();
            Property(t => t.Observations).HasColumnName("Observations");

            //Relationships
            HasRequired(t => t.Director)
                .WithMany(t => t.Moovies)
                .HasForeignKey(k => k.DirectorId).WillCascadeOnDelete(false);
            HasRequired(t => t.Country)
                .WithMany(t => t.Moovies)
                .HasForeignKey(k => k.CountryId).WillCascadeOnDelete(false);
            HasRequired(t => t.Genre)
                .WithMany(t => t.Moovies)
                .HasForeignKey(k => k.GenreId).WillCascadeOnDelete(false);
        }
    }
}
