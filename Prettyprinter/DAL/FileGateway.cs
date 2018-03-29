using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class FileGateway : DataGateway<File>
    {
        public FileGateway(ApplicationDbContext context) : base(context)
        {

        }
    }
}
