using System;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class FileBuilder : DocumentBuilder
    {
        //Because of Stub Datalink being Entity Framework's default DB, it will be complicated to store two entity in a database
        Folder file;
        ApplicationDbContext db;

        public override void BuildDocument(ApplicationDbContext context, string fileID, string userID, string parentID, string Name)
        {
            db = context;
            string accessControlID = Guid.NewGuid().ToString();
            AccessControl accessControl = new AccessControl(accessControlID, fileID, userID, true, true);
            file = new Folder(fileID, parentID, Name, accessControl);
            file.type = 1;
        }

        public override Document GetDocument()
        {
            return file;
        }

        public void BuildContent(String content)
        {
            SaveDocument();
            //fileManager.populateDocument(id, content);
        }

        public override void SaveDocument()
        {
            FolderGateway folderGateway = new FolderGateway(db);
            folderGateway.CreateFile(file);

            FileManager fileManager = new FileManager();
            fileManager.createDocument(file.ParentId, file._id);
        }
    }
}
