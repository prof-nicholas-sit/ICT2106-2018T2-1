﻿using System;
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
        //private readonly ApplicationDbContext _context;
        private static String serverPath = @"2107 File Server";
        public FolderController(ApplicationDbContext context)
        {
            folderGateway = new FolderGateway(context);
        }

        // GET: Folder
        public ActionResult Index()
        {
            //System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
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
            DateTime dateNow = DateTime.Now;
            AccessController accessController = new AccessController();
            accessController.createMetaData(new MetaData("", name, parentId, type, new string[4], dateNow));
            Folder folder = new Folder();
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
        public ActionResult DeleteConfirmed(string id)
        {
            folderGateway.DeleteFile(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(string id)
        {
            return (folderGateway.SelectById(id) != null);
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
        public static Boolean createFolder(String location, String folderName)
        {
            String path = serverPath + location;
            

            String pathToFile = path;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

           // Console.WriteLine("\n\n");
            foreach (String line in AllEntries)
            {
                String currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(folderName))
                {
                   
                    return false;
                }
            }
            Directory.CreateDirectory(path + @"\" + folderId);
            return true;
        }


        //CREATE A NEW TXT FILE
        public static Boolean createFile(String location, String folderName)
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
        public static Boolean deleteFile(String location, String fileName)
        {

            if (System.IO.File.Exists(serverPath + location + @"\" + fileId + ".txt"))
            {
                System.IO.File.Delete(serverPath + location + @"\" + fileId + ".txt");
            }
            return true;
        }

        //DELETE A FOLDER
        public static void deleteFolder(String location, String fileName)
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
