using Sys10.Data.Models.Mapping;
using Sys10.Data.Programmability.Functions;
using Sys10.Data.Programmability.Stored_Procedures;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace Sys10.Data.Context
{
    public partial class Sys10Context
    {
        public StoredProcedures StoredProcedures { get; set; }
        public ScalarValuedFunctions ScalarValuedFunctions { get; set; }
        public TableValuedFunctions TableValuedFunctions { get; set; }
    }
}
