using Sys10.Data.Context;
using Sys10.Data.Models;
using Sys10.Data.Repository._BulkBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Sys10.Data.Repository.Bulk
{
    public class BulkBase<TEntity> : IDisposable, IBulkBase<TEntity> where TEntity : class
    {
        internal Sys10Context Context;
        internal List<TEntity> EntitiesToInsert;
        internal List<TEntity> EntitiesToUpdate;
        internal SqlTransaction SqlTransaction;

        public BulkBase()
        {
            this.EntitiesToInsert = new List<TEntity>();
            OpenConnection();
        }

        private void OpenConnection()
        {
            var DB = new Sys10Database("Sys10Context", 20000);
            var connection = DB.Connection;
            connection.Open();

            var randomNumber = new Random();
            var transactionName = $"Transaction_{randomNumber.Next()}";

            SqlTransaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, transactionName);
        }

        public void Add(TEntity entity)
        {
            EntitiesToInsert.Add(entity);
        }

        public void AddAll(List<TEntity> entities)
        {
            EntitiesToInsert.AddRange(entities);
        }

        public void Edit(TEntity entity)
        {
            EntitiesToUpdate.Add(entity);
        }
        
        public void EditAll(List<TEntity> entities)
        {
            EntitiesToUpdate.AddRange(entities);
        }

        public void Dispose()
        {
            EntitiesToInsert = null;
            EntitiesToUpdate = null;
            //Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
