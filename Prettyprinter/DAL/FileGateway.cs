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
        public new void MoveFile(string fileId, string parentId)
        {
            File file = data.Find(fileId);
            file.parentId = parentId;
            data.Update(file);
        }
        public new void RenameFile(string fileId, string fileName)
        {
            File file = data.Find(fileId);
            file.name = fileName;
            data.Update(file);
            base.SaveChanges();
        }
    }
}
