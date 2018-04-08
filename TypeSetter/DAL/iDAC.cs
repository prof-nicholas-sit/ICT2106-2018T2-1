using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypeSetter.DAL
{
    interface iDAC
    {
        bool checkPermission(string FileName, string Action);
    }
}
