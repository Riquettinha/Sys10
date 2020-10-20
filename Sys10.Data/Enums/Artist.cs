using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Enums
{
    public class Artist
    {
        public enum Type
        {
            [Description("Diretor")]
            Director = 0,
            [Description("Ator")]
            Actor = 1

            //Etc...
        }
    }
}
