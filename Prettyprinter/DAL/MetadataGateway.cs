using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class MetadataGateway : DataGateway<Metadata>
    {
        public MetadataGateway(ApplicationDbContext context) : base(context) { }
    }
}
