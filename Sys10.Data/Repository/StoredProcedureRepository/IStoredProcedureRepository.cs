using Sys10.Data.Models;
using Sys10.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Repository
{
    public interface IStoredProcedureRepository
    {
        bool AddMoovie();
    }
}
