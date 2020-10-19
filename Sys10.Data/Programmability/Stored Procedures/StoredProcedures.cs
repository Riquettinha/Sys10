using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Sys10.Data.Programmability.Stored_Procedures
{
    public class StoredProcedures : Models.ModelBase
    {
        private readonly DbContext _dbContext;
        public StoredProcedures(DbContext dbContext) : base("", "")
        {
            _dbContext = dbContext;
        }
    }
}
