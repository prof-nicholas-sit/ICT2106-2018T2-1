using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class AccessControlGateway : DataGateway<AccessControl>
    {
        public AccessControlGateway(ApplicationDbContext context) : base(context) { }
    }
}
