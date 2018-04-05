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
        private const String serverDirectory = @"2107 File Server\";
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
            }
        }

        // GET: Document
        public ActionResult Index(String param, String id)
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

            //Retrieve last part of Server Path (Folder ID)
            string[] currentServerPath = serverPath.Split("/");
            if(currentServerPath.Length >= 2)
            {
                return View(folderGateway.SelectAll(currentServerPath[currentServerPath.Length-1], "ParentID"));
            }
            return View(folderGateway.SelectAll(serverPath, "ParentID"));
        }


        // Create for both File and Folder
        public ActionResult Create(string docName, string creationPath, int isFile)
        {
            string parentId;
            string name = docName;
            string id = Guid.NewGuid().ToString();
            
            if (!String.IsNullOrEmpty(creationPath))
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

            AccessControl accessControl = new AccessControl(
                   Guid.NewGuid().ToString(), id, currentUserID, true, true);

            List<AccessControl> accessControls = new List<AccessControl>();
            accessControls.Add(accessControl);


            Folder document = new Folder(id, parentId, name, accessControl);

            // 0 = Folder, 1 = File
            if (isFile == 0)
            {
                Metadata metadata = new Metadata(id, currentUserID, docName, document.type, "", parentId, accessControls);
                new MetadataController(applicationDbContext).AddMetadata(metadata);

                //Storing Folder's Meta Data(Stub Method to store both Folder and File into SQL)
                folderGateway.CreateFile(document);

                //Create Folder on physical server
                folderManager.createDocument(creationPath, id);
            }
            // File
            else
            {
                //Specify type is File
                document.type = 1;

                // Sent Data over to the typesetter , when creation of File
                TypeSetterController action = new TypeSetterController();
                FileController data = action.onCreate();
                data.setParentId(parentId);
                
                Metadata metadata = new Metadata(id, currentUserID, name, document.type, "", parentId, accessControls);
                new MetadataController(applicationDbContext).AddMetadata(metadata);

                //Storing File's Meta Data (Stub Method to store both Folder and File into SQL)
                folderGateway.CreateFile(document);

                //Create File on physical server
                fileManager.createDocument(creationPath, id);
            }

            //Retrieve last part of Path (Human-Readable)
            string[] lastPath = HttpContext.Session.GetString("Path").Split("/");
            string pathParam = (lastPath.Length >= 2) 
                ? lastPath[lastPath.Length - 1] : "Root";

            //Redirect to Index to update display
            return RedirectToAction(nameof(Index), new { param = pathParam, id = parentId });
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

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Move(string moveId, string movePath)
        {
            folderGateway.MoveFile(moveId, movePath);
            fileManager.moveDocument(HttpContext.Session.GetString("serverPath"), movePath, moveId);
            return RedirectToAction(nameof(Index));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy(string copyId, string copyPath)
        {
            string createdId = folderGateway.CopyFile(copyId, copyPath);
            fileManager.copyDocument(HttpContext.Session.GetString("serverPath"), copyPath, copyId);
            return RedirectToAction(nameof(Index));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string renameId, string newName)
        {
            folderGateway.RenameFile(renameId, newName);
            return RedirectToAction(nameof(Index));
        }

        //private bool FolderExists(string id)
        //{
        //    return (folderGateway.SelectById(id) != null);
        //}
    }
}
