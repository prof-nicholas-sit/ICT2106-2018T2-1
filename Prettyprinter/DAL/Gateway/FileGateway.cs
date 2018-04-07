using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    //Stub gateway for DataLink, storing File's metadata
    public class FileGateway : DataGateway<File>
    {
        private readonly ApplicationDbContext db;
        public FileGateway(ApplicationDbContext context) : base(context)
        {
            db = context;
        }
        public void CreateFile(File obj)
        {
            db.Add(obj);
            db.SaveChanges();
        }
    }
}
