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
    public class FolderController : Controller
    {
        public FolderGateway folderGateway;
        //private readonly ApplicationDbContext _context;
        private static String serverPath = @"2107 File Server\";
        public FolderController(ApplicationDbContext context)
        {
            folderGateway = new FolderGateway(context);
            if (!System.IO.File.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            //if (!System.IO.File.Exists(serverPath + HttpContext.Session.GetString("currentUserID"))){
            //    Directory.CreateDirectory(serverPath + HttpContext.Session.GetString("currentUserID"));
            //}
            if (!System.IO.File.Exists(serverPath + "root"))
            {
                Directory.CreateDirectory(serverPath + "root");
            }
        }

        // GET: Folder
        public ActionResult Index(String param,String id)
        {
            var path = "";
            //System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
            if (String.IsNullOrEmpty(param))
            {
                if (HttpContext.Session.GetString("Path") == null)
                    HttpContext.Session.SetString("Path", "root");
                else
                    HttpContext.Session.SetString("Path", "root");

            }
            else
            {
                //Append the SESSION PATH
                var currentPath = HttpContext.Session.GetString("Path") + "/" + param;
                HttpContext.Session.SetString("Path", currentPath);
               
            }
            path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;

            return View(folderGateway.SelectAll(path));

        }


        // POST: Folder/Create
        public ActionResult Create(string folderName,String theParentID)
        {
            string parentId = HttpContext.Session.GetString("Path");

            //This is the part im abit confused
            System.Diagnostics.Debug.WriteLine("*** HAHA" + theParentID, "HAHA");
            if (!String.IsNullOrEmpty(theParentID)) {
                parentId = theParentID;
            }

            string type = "folder";
            string name = folderName;
            string id = Guid.NewGuid().ToString();
            DateTime dateNow = DateTime.Now;
            AccessController accessController = new AccessController();
            accessController.createMetaData(new MetaData("", name, parentId, type, new string[4], dateNow));
            Folder folder = new Folder();
            folder._id = id;
            folder.parentId = parentId;
            folder.type = "folder";
            folder.name = name;
            folder.accessControl = new string[4];
            folder.date = dateNow;

            folderGateway.CreateFile(folder);
            createFolder(parentId, id);
            
            return RedirectToAction(nameof(Index));
        }

        // POST: Folder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string deleteId)
        {
            folderGateway.DeleteFile(deleteId);
            deleteFile(HttpContext.Session.GetString("serverPath"), deleteId);
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(string id)
        {
            return (folderGateway.SelectById(id) != null);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Move(string moveId, string movePath)
        {
            folderGateway.MoveFile(moveId, movePath);
            return RedirectToAction(nameof(Index));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Copy(string copyId, string copyPath)
        {
            string createdId = folderGateway.CopyFile(copyId, copyPath);
            createFolder(createdId, copyPath);
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






        // ================================================= FILE SERVER MANAGER METHODS ===================================================

        // READ A TXT FILE
        public static void ReadTextFile(String file)
        {
            String pathToFile = serverPath + @"\" + file + ".txt";
            List<String> lines = System.IO.File.ReadAllLines(pathToFile).ToList();
        
        }

        //GET ALL FOLDERS
        public static void getAllFolders(String file)
        {
            String pathToFile = serverPath + file;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            foreach (String line in AllEntries)
            {
                String folderId = line;
                folderId = folderId.Replace(pathToFile + @"\", "");
                System.Diagnostics.Debug.WriteLine("*** HAHA111 - " + folderId, "HAHA");
            }
        

        }

        //GET ALL FILES
        public static void getAllFiles(String file)
        {
            String pathToFile = serverPath + file;
            List<String> AllEntries = Directory.GetFiles(pathToFile).ToList();

            foreach (String line in AllEntries)
            {
                String folderName = line;
                folderName = folderName.Replace(pathToFile + @"\", "");

                Console.WriteLine(folderName);
            }
            Console.WriteLine("\n");

        }

        //GET PARENT PATH OF FILE/FOLDER
        public static String getParentOfFile(String parent)
        {
            String ParentName = Directory.GetParent(parent).ToString();
            return ParentName;
        }

        //CREATE A NEW FOLDER
        public static Boolean createFolder(String location, String folderId)
        {
            String path = serverPath + location;
            

            String pathToFile = path;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

           // Console.WriteLine("\n\n");
            foreach (String line in AllEntries)
            {
                String currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(folderId))
                {
                   
                    return false;
                }
            }
            Directory.CreateDirectory(path + @"\" + folderId);
            return true;
        }

        //CREATE A NEW TXT FILE
        public static Boolean createFile(String location, String fileId)
        {
            Console.WriteLine("\n");
            String pathToFile = serverPath +  location + @"\" + fileId + ".txt";
            if (System.IO.File.Exists(pathToFile))
            {

                return false;
            }

            System.IO.File.AppendAllText(pathToFile, "");
            return true;
        }

        //DELETE A TXT FILE
        public static Boolean deleteFile(String location, String fileId)
        {

            if (System.IO.File.Exists(serverPath + location + @"\" + fileId + ".txt"))
            {
                System.IO.File.Delete(serverPath + location + @"\" + fileId + ".txt");
            }
            return true;
        }

        //DELETE A FOLDER
        public static void deleteFolder(String location, String fileId)
        {
            try
            {
                var dir = new DirectoryInfo(serverPath + location + @"\" + fileId);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //RENAMING A FOLDER
        public static Boolean renameFolder(String location, String oldName, String newName)
        {
            if (!System.IO.File.Exists(serverPath + location + @"\" + newName))
            {
                Directory.Move(serverPath + location + @"\" + oldName, serverPath + location + @"\" + newName);
                return true;
            }
            return false;
        }

        //RENAMING A TXT FILE
        public static Boolean renameFile(String location, String oldName, String newName)
        {
            if (!System.IO.File.Exists(serverPath + location + @"\" + newName + ".txt"))
            {
                System.IO.File.Move(serverPath + location + @"\" + oldName + ".txt", serverPath + location + @"\" + newName + ".txt");
                return true;
            }
            return false;
        }

        //MOVING A TXT FILE FROM ONE PATH TO ANOTHER
        public static Boolean moveFile(String location, String newLocation, String oldName)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + oldName + ".txt"))
            {
                System.IO.File.Move(serverPath + location + @"\" + oldName + ".txt", serverPath + newLocation + @"\" + oldName + ".txt");
                return true;
            }
            return false;
        }

        //COPYING A TXT FILE FROM ONE PATH TO ANTOHER
        public static Boolean copyFile(String location, String newLocation, String oldName)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + oldName + ".txt"))
            {
                System.IO.File.Copy(serverPath + location + @"\" + oldName + ".txt", serverPath + newLocation + @"\" + oldName + ".txt");
                return true;

            }
            return false;
        }


        // MOVE FROM ONE PATH TO ANTOHER
        // add to new place
        // Remove current one
        
        public static Boolean MoveFile(String location, String newLocation, String oldName)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + oldName + ".txt"))
            {

                System.IO.File.Move(serverPath + location + @"\" + oldName + ".txt", serverPath + newLocation + @"\" + oldName + ".txt");
                return true;

            }
            return false;
        }


    }
}
