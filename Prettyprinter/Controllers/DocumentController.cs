using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class DocumentController : Controller
    {
        public FolderManager folderManager = new FolderManager();
        public FileManager fileManager = new FileManager();

        public FolderGateway folderGateway;
        public ApplicationDbContext applicationDbContext;

        private static String serverDirectory = @"2107 File Server\";
        private static String currentUserID = "161616";


        public DocumentController(ApplicationDbContext context)
        {
            folderGateway = new FolderGateway(context);
            applicationDbContext = context;
            
            if (!System.IO.File.Exists(serverDirectory + currentUserID))
            {
                Directory.CreateDirectory(serverDirectory + currentUserID);
            }
        }

        // GET: Folder
        public ActionResult Index(String param, String id)
        {
            var path = "";
            var serverPath = "";
            //System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
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
                    //Append the SESSION PATH
                    var newServerPath = HttpContext.Session.GetString("ServerPath") + "/" + id;
                    HttpContext.Session.SetString("ServerPath", newServerPath);

                    var newPath = HttpContext.Session.GetString("Path") + "/" + param;
                    HttpContext.Session.SetString("Path", newPath);
                }
            }

            path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;

            serverPath = HttpContext.Session.GetString("ServerPath");
            ViewBag.serverPath = serverPath;

            string[] currentServerPath = serverPath.Split("/");
            if(currentServerPath.Length >= 2)
            {
                Debug.WriteLine("ENTER : "+ currentServerPath[currentServerPath.Length - 1]);
                return View(folderGateway.SelectAll(currentServerPath[currentServerPath.Length-1], "ParentID"));
            }
            return View(folderGateway.SelectAll(serverPath, "ParentID"));
        }


        // POST: Folder/Create
        public ActionResult Create(string docName, string creationPath, int isFile)
        {
            Debug.WriteLine("DEBUG : " + docName + " : " + creationPath + " : "+isFile);
            string parentId;
            string id = Guid.NewGuid().ToString();
            string name = docName;
            DateTime dateNow = DateTime.Now;


            //This is the part im abit confused
            if (!String.IsNullOrEmpty(creationPath))
            {
                //Get last part of creationPath
                string[] parentPath = creationPath.Split("/");
                parentId = parentPath[parentPath.Length-1];
            }
            else
            {
                //parentId = HttpContext.Session.GetString("Path");
                parentId = currentUserID;
            }
            System.Diagnostics.Debug.WriteLine(parentId);

            AccessControl accessControl = new AccessControl(
                   Guid.NewGuid().ToString(),
                   id,
                   currentUserID,
                   true,
                   true);

            List<AccessControl> accessControls = new List<AccessControl>();
            accessControls.Add(accessControl);

            Folder folder = new Folder();
            folder._id = id;
            folder.parentId = parentId;
            folder.name = name;
            folder.accessControl = new string[4];
            folder.date = dateNow;

            // 0 = Folder, 1 = File
            if (isFile == 0)
            {

           
                Metadata metadata = new Metadata(id, currentUserID, docName, Folder.TYPE, dateNow, "", parentId, accessControls);

                //FileController fc = new FileController();
                //fc.createFile(parameters);


                folder.type = Folder.TYPE;

                folderGateway.CreateFile(folder);

                new MetadataController(applicationDbContext).AddMetadata(metadata);
                //Create a real folder locally in file server
                folderManager.createDocument(parentId, id);

                //return RedirectToAction(nameof(Index), new { param = HttpContext.Session.GetString("Path"), id = folder.parentId });

            }
            // File
            else
            {
                // Sent Data over to the typesetter , when creation of File

                TypeSetterController action = new TypeSetterController();
                FileController data = action.onCreate();
                data.setParentId(parentId);

                Metadata metadata = new Metadata(id, currentUserID, data.getName(), 1, dateNow, "", parentId, accessControls);

                // Set File Type
                folder.type = 1;

                folderGateway.CreateFile(folder);

                new MetadataController(applicationDbContext).AddMetadata(metadata);
                //Create a real folder locally in file server
                folderManager.createDocument(parentId, id);
            }

            //Create a real folder locally in file server
            folderManager.createDocument(creationPath, id);
            
            string[] lastPath = HttpContext.Session.GetString("Path").Split("/");

            string pathParam = (lastPath.Length >= 2) 
                ? lastPath[lastPath.Length - 1] : "Root";

            return RedirectToAction(nameof(Index), new { param = pathParam, id = parentId });
        }

        // POST: Folder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string deleteId)
        {
            folderGateway.DeleteFile(deleteId);
            //Delete the file locally from file server
            fileManager.deleteDocument(HttpContext.Session.GetString("serverPath"), deleteId);
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
