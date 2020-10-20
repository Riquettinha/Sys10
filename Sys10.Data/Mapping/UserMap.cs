using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sys10.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(500).IsRequired();
            Property(t => t.Password).HasColumnName("Password").HasMaxLength(500).IsRequired();
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.AuthenticationToken).HasColumnName("AuthenticationToken").HasMaxLength(6);
            Property(t => t.AuthenticationTokenExpiration).HasColumnName("AuthenticationTokenExpiration");
        }
    }
}
