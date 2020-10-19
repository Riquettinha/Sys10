using Sys10.Data.Bulk;
using Sys10.Data.Context;
using Sys10.Data.Models;
using Sys10.Data.Repository;
using Sys10.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly ISys10Context _context;

        public Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }
        public IStoredProcedureRepository StoredProcedure { get; set; }

        public UnitOfWork()
        {
            _context = new Sys10Context();
            var bulkWorker = new BulkWorker<ModelBase>(_context);
            RepositoryBase = new Repository.Base.RepositoryBase<ModelBase>(_context, bulkWorker);
            StoredProcedure = new StoredProcedureRepository(_context);
        }

        private bool _disposed;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}
