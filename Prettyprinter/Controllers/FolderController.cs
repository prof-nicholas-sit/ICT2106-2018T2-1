using System;
using System.Collections.Generic;
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
        private readonly ApplicationDbContext _context;
        private static String serverPath = @"2107 File Server\";
        public FolderController(ApplicationDbContext context)
        {
            folderGateway = new FolderGateway(context);
        }

        // GET: Folder
        public ActionResult Index()
        {
            //System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
            //HttpContext.Session.SetString("serverPath", HttpContext.Session.GetString("currentUserID"));
            if (HttpContext.Session.GetString("serverPath") == null)
                HttpContext.Session.SetString("serverPath", "currentUserID");
            var serverPath = HttpContext.Session.GetString("serverPath");
            ViewBag.SeverPath = serverPath;

            if (HttpContext.Session.GetString("Path") == null)
                HttpContext.Session.SetString("Path", "root");
            var path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;
            return View(folderGateway.SelectAll(path));
        }

        // POST: Folder/Create
        public ActionResult Create(string folderName)
        {
            string parentId = HttpContext.Session.GetString("Path");
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

        // ================================================= FILE SERVER MANAGER METHODS ===================================================

        // READ A TXT FILE
        public static void ReadTextFile(String fileId)
        {
            String pathToFile = serverPath + @"\" + fileId + ".txt";
            List<String> lines = System.IO.File.ReadAllLines(pathToFile).ToList();
            foreach (String line in lines)
            {
                Console.WriteLine(line);
            }
        }

        //GET ALL FOLDERS
        public static void getAllFolders(String fileId)
        {
            String pathToFile = serverPath + fileId;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            foreach (String line in AllEntries)
            {
                String folderId = line;
                folderId = folderId.Replace(pathToFile + @"\", "");

                Console.WriteLine(folderId);
            }
            Console.WriteLine("\n");

        }

        //GET ALL FILES
        public static void getAllFiles(String fileId)
        {
            String pathToFile = serverPath + fileId;
            List<String> AllEntries = Directory.GetFiles(pathToFile).ToList();

            foreach (String line in AllEntries)
            {
                String folderId = line;
                folderId = folderId.Replace(pathToFile + @"\", "");

                Console.WriteLine(folderId);
            }
            Console.WriteLine("\n");

        }

        //GET PARENT PATH OF FILE/FOLDER
        public static String getParentOfFile(String fileId)
        {
            String parentId = Directory.GetParent(fileId).ToString();
            return parentId;
        }

        //CREATE A NEW FOLDER
        public static Boolean createFolder(String location, String folderId)
        {
            Directory.CreateDirectory(location + @"\" + folderId);

            String pathToFile = location;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            Console.WriteLine("\n\n");
            foreach (String line in AllEntries)
            {
                String currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(folderId))
                {
                    return false;
                }
            }

            return true;
        }


        //CREATE A NEW TXT FILE
        public static Boolean createFile(String location, String fileId)
        {
            Console.WriteLine("\n");

            String pathToFile = location + @"\" + fileId + ".txt";
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

            if (System.IO.File.Exists(location + @"\" + fileId + ".txt"))
            {
                System.IO.File.Delete(location + @"\" + fileId + ".txt");
            }
            return true;
        }

        //DELETE A FOLDER
        public static void deleteFolder(String location, String fileId)
        {
            try
            {
                var dir = new DirectoryInfo(location + @"\" + fileId);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //MOVING A TXT FILE FROM ONE PATH TO ANOTHER
        public static Boolean moveFile(String location, String newLocation, String fileId)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + fileId + ".txt"))
            {
                System.IO.File.Move(serverPath + location + @"\" + fileId + ".txt", serverPath + newLocation + @"\" + fileId + ".txt");
                return true;
            }
            return false;
        }

        //COPYING A TXT FILE FROM ONE PATH TO ANTOHER
        public static Boolean copyFile(String location, String newLocation, String fileId)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + fileId + ".txt"))
            {
                System.IO.File.Copy(serverPath + location + @"\" + fileId + ".txt", serverPath + newLocation + @"\" + fileId + ".txt");
                return true;
            }
            return false;
        }










    }
}
