using System;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class FileBuilder : DocumentBuilder
    {
        //Because of Stub Datalink being Entity Framework's default DB, it will be complicated to store two entity in a database
        //Thus, using Folder class to store File's metadata (to share the same Table in DB)
        Folder file;
        ApplicationDbContext db;
        FileManager fileManager = new FileManager();
        string creationPath;

        public override void BuildDocument(ApplicationDbContext context, string fileID, string userID, string creationPath,
            string parentID, string Name)
        {
            //Initialising components
            db = context;
            this.creationPath = creationPath;
            string accessControlID = Guid.NewGuid().ToString();
            AccessControl accessControl = new AccessControl(accessControlID, fileID, userID, true, true);
            file = new Folder(fileID, parentID, Name, accessControl);
            file.type = 1;
        }

        public override Document GetDocument()
        {
            return file;
        }

        public void BuildContent(string content)
        {
            SaveDocument();
            //Writing the content to the physical files in server
            fileManager.writeDocument(creationPath, content, file._id, true);
        }

        public override void SaveDocument()
        {
            //To share the same table with folder, using FolderGateway
            FolderGateway folderGateway = new FolderGateway(db);
            folderGateway.CreateFile(file);

            //To create the physical file using FileManager
            fileManager.createDocument(creationPath, file._id);
        }
    }
}
