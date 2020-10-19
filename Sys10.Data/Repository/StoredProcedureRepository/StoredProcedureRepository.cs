using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Sys10.Data.Context;
using Sys10.Data.Models;

namespace Sys10.Data.Repository
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly ISys10Context _context;

        public StoredProcedureRepository(ISys10Context context) 
        {
            _context = context;
        }

        public bool AddMoovie()
        {
            return true;
        }

    }
}
