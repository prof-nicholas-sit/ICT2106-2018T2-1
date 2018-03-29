using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class FileGateway : DataGateway<Files>
    {
        public FileGateway(ApplicationDbContext context) : base(context)
        {

        }
        public new void MoveFile(string fileId, string parentId)
        {
            Files file = data.Find(fileId);
            file.parent = parentId;
            data.Update(file);
        }
        public new void RenameFile(string fileId, string fileName)
        {
            Files file = data.Find(fileId);
            file.name = fileName;
            data.Update(file);
            base.SaveChanges();
        }
    }
}
