using Sys10.Data.Models;
using Sys10.Data.Programmability.Functions;
using Sys10.Data.Programmability.Stored_Procedures;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sys10.Data.Context
{
    public interface ISys10Context : IDisposable
    {
        Database Database { get; }
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbContextConfiguration Configuration { get; }
        DbSet<User> User { get; set; }
        int SaveChanges();
    }
}