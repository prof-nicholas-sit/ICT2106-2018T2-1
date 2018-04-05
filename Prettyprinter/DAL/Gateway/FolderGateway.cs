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
            folder.ParentId = parentId;
            data.Update(folder);
            base.SaveChanges();
        }

        public void RenameFile(string renameId, string newName)
        {
            Folder folder = data.Find(renameId);
            folder.Name = newName; 
            data.Update(folder);
            base.SaveChanges();
        }

        public string CopyFile(string fileId, string parentId)
        {
            Folder folder = data.Find(fileId);
            folder.ParentId = parentId;
            folder._id = Guid.NewGuid().ToString();
            data.Add(folder);
            return folder._id;
        }
    }
}
