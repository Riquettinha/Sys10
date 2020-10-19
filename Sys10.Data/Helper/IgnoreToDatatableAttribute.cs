﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreToDatatableAttribute : Attribute
    {
        public bool IgnorePropertyToDatatable { get; set; }

        public IgnoreToDatatableAttribute()
        {
        }

    }
}
