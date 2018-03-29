using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class FolderGateway : DataGateway<Folder>
    {
        public FolderGateway(ApplicationDbContext context) : base(context) { }
        public void MoveFile(string fileId, string parentId)
        {
            Folder folder = data.Find(fileId);
            folder.parentId = parentId;
            data.Update(folder);
        }
        public void RenameFile(string fileId, string fileName)
        {
            Folder folder = data.Find(fileId);
            folder.name = fileName; 
            data.Update(folder);
            base.SaveChanges();
        }
        public string CopyFile(string fileId, string parentId)
        {
            Folder folder = data.Find(fileId);
            folder.parentId = parentId;
            folder._id = Guid.NewGuid().ToString();
            data.Add(folder);
            return folder._id;
        }
    }
}
