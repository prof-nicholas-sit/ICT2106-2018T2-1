using System;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class FolderBuilder : DocumentBuilder
    {
        Folder folder;
        ApplicationDbContext db;
        string creationPath;

        public override void BuildDocument(ApplicationDbContext context, string folderID, string userID, string creationPath,
            string parentID, string Name, Boolean permission)
        {
            //Initialising components
            db = context;
            this.creationPath = creationPath;
            string accessControlID = Guid.NewGuid().ToString();
            AccessControl accessControl = new AccessControl(accessControlID, folderID, userID, permission, permission);
            folder = new Folder(folderID, parentID, Name, accessControl);
        }

        public override Document GetDocument()
        {
            return folder;
        }

        public override void SaveDocument()
        {
            //Storing Folder's Meta Data(Stub Method to store both Folder and File into SQL)
            FolderGateway folderGateway = new FolderGateway(db);
            folderGateway.CreateFile(folder);

            //Create Folder on physical server
            FolderManager folderManager = new FolderManager();
            folderManager.createDocument(creationPath, folder._id);
        }
    }
}
