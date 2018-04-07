using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class DocumentController : Controller
    {
        //File Storage
        public FolderManager folderManager = new FolderManager();
        public FileManager fileManager = new FileManager();

        //DAL
        public FolderGateway folderGateway;
        public FileGateway fileGateway;
        public ApplicationDbContext applicationDbContext;

        //Constants
        private const string serverDirectory = @"2107 File Server\";

        //Stub constant to represent getting userID from Session
        private const String currentUserID = "161616";

        //Constructor
        public DocumentController(ApplicationDbContext context)
        {
            //Initialising DAL
            folderGateway = new FolderGateway(context);
            fileGateway = new FileGateway(context);
            applicationDbContext = context;
            
            //Create folder if not exist
            if (!System.IO.File.Exists(serverDirectory + currentUserID))
            {
                Directory.CreateDirectory(serverDirectory + currentUserID);
                //Create a shared folder
                Directory.CreateDirectory(serverDirectory + currentUserID + @"\" + currentUserID +".SHARED");
            }
          
        }

        // GET: Document
        public ActionResult Index(string param, string id)
        {
            var path = "";
            var serverPath = "";

            //Root Page
            if (String.IsNullOrEmpty(param))
            {
                HttpContext.Session.SetString("ServerPath", currentUserID);
                HttpContext.Session.SetString("Path", "Root");
            }
            else
            {
                //Catch if simply refresh
                if (!HttpContext.Session.GetString("ServerPath").Contains(id))
                {
                    //Both Path and Server Path
                    var newServerPath = HttpContext.Session.GetString("ServerPath") + "/" + id;
                    HttpContext.Session.SetString("ServerPath", newServerPath);

                    var newPath = HttpContext.Session.GetString("Path") + "/" + param;
                    HttpContext.Session.SetString("Path", newPath);
                }
            }

            //Set updated Path and Server Path to ViewBag
            path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;

            serverPath = HttpContext.Session.GetString("ServerPath");
            ViewBag.serverPath = serverPath;

            ViewBag.user = currentUserID;

            //Retrieve last part of Server Path (Folder ID)
            string[] currentServerPath = serverPath.Split("/");
            if(currentServerPath.Length >= 2)
            {
                return View(folderGateway.SelectAll(currentServerPath[currentServerPath.Length-1], "ParentID"));
            }
            return View(folderGateway.SelectAll(serverPath, "ParentID"));
        }


        // Create for both File and Folder
        public ActionResult Create(string docName, string creationPath, int isFile, bool permission)
        {
            string parentId;
            string name = docName;
            string id = Guid.NewGuid().ToString();
            string sourceId = ""; //For Sharing
            

            if (permission == false)
            {
                string[] parentPath = creationPath.Split("\\");

                // Make the shared file parent ID to the [personID].[SHARED]
                sourceId = parentPath[0];
                parentId =  parentPath[2];
                creationPath = parentPath[1] + @"\" + parentPath[2];
        

            }
            else if (!String.IsNullOrEmpty(creationPath))
            {
                //If not root - get last part of creationPath
                string[] parentPath = creationPath.Split("/");
                parentId = parentPath[parentPath.Length-1];
            }
            
            else
            {
                //Else - Set folder is root
                parentId = currentUserID;
            }

            AccessControl accessControl = new AccessControl(Guid.NewGuid().ToString(), id, currentUserID, permission, permission);

            List<AccessControl> accessControls = new List<AccessControl>();
            accessControls.Add(accessControl);


            Folder document = new Folder(id, parentId, name, accessControl);

            // 0 = Folder, 1 = File
            if (isFile == 0)
            {
                Metadata metadata = new Metadata(id, currentUserID, docName, document.type, "", parentId, accessControls);
                new MetadataController(applicationDbContext).AddMetadata(metadata);

                FolderBuilder folderBuilder = new FolderBuilder();
                folderBuilder.BuildDocument(applicationDbContext, id, currentUserID, creationPath, parentId, name,true);
                folderBuilder.SaveDocument();
            }
            // File
            else
            {
                //Specify type is File
                document.type = 1;

                // Simulate TypeSetter Controller Stub
                TypeSetterController typeSetterController = new TypeSetterController();
                FileBuilder fileBuilder = new FileBuilder();
                fileBuilder.BuildDocument(applicationDbContext, id, currentUserID, creationPath, parentId, name, true);
                
                //Stub to simulate passing builder over to Typesetter and calling Builder's BuildContent() and SaveDocument()
                typeSetterController.onCreate(fileBuilder);

                //replace content
                if (permission == false)
                {
                    string sourcePath = HttpContext.Session.GetString("ServerPath");
                    string lines = fileManager.readDocument(sourcePath, sourceId);

                    fileManager.writeDocument(creationPath, lines, id, true);
                    
                }
                
                Metadata metadata = new Metadata(id, currentUserID, name, document.type, "", parentId, accessControls);

                //Stub method to add metadata through AccessControl
                new MetadataController(applicationDbContext).AddMetadata(metadata);
            }

            //Retrieve last part of Path (Human-Readable)
            string[] lastPath = HttpContext.Session.GetString("Path").Split("/");
            string pathParam = (lastPath.Length >= 2) 
                ? lastPath[lastPath.Length - 1] : "Root";

            if (permission == true) { 
            //Redirect to Index to update display
            return RedirectToAction(nameof(Index), new { param = pathParam, id = parentId });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // Delete for both File and Folder
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string deleteId)
        {
            //Delete 
            folderGateway.DeleteFile(deleteId);
            //Delete the file locally from file server
            fileManager.deleteDocument(HttpContext.Session.GetString("serverPath"), deleteId);

            //Retrieve last part of Path (Human-Readable)
            string[] lastPath = HttpContext.Session.GetString("Path").Split("/");
            string pathParam = (lastPath.Length >= 2)
                ? lastPath[lastPath.Length - 1] : "Root";

            return RedirectToAction(nameof(Index));
        }

        //Move File Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Move(string moveId, string movePath)
        {
            folderGateway.MoveFile(moveId, movePath);
            fileManager.moveDocument(HttpContext.Session.GetString("serverPath"), movePath, moveId);
            return RedirectToAction(nameof(Index));
        }

        //Copy File Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy(string copyId, string copyPath)
        {
            string createdId = folderGateway.CopyFile(copyId, copyPath);
            fileManager.copyDocument(HttpContext.Session.GetString("serverPath"), copyPath, copyId);
            return RedirectToAction(nameof(Index));
        }

        //Share File Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Share(string userId, string fileId, string fileName)
        {
            string sourceIdDest = fileId + @"\" + userId + @"\" + userId + ".SHARED";
            return RedirectToAction("Create", "Document", new { docName = fileName, creationPath = sourceIdDest, isFile = 1, permission = false });
            
        }

        //Rename Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string renameId, string newName)
        {
            folderGateway.RenameFile(renameId, newName);
            return RedirectToAction(nameof(Index));
        }
    }
}
