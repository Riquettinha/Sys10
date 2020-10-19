using Sys10.Data.Models;
using Sys10.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }

        IStoredProcedureRepository StoredProcedure { get; set; }

        void Commit();
    }
}
