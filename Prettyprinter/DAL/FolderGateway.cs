using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class FolderGateway : DataGateway<Folder>
    {
        public FolderGateway(ApplicationDbContext context) : base(context)
        {

        }
    }
}
