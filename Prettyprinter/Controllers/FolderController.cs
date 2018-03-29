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
        private readonly ApplicationDbContext _context;
        private static String serverPath = @"2107 File Server";

        public FolderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Folder
        public async Task<IActionResult> Index()
        {
            System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
            var path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;
            return View(await _context.Folder.ToListAsync());
        }

        // GET: Folder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder
                .SingleOrDefaultAsync(m => m._id == id);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // GET: Folder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("_id,name,parentId,type,date")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder.SingleOrDefaultAsync(m => m._id == id);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        // POST: Folder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("_id,name,parentId,type,date")] Folder folder)
        {
            if (id != folder._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolderExists(folder._id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder
                .SingleOrDefaultAsync(m => m._id == id);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // POST: Folder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var folder = await _context.Folder.SingleOrDefaultAsync(m => m._id == id);
            _context.Folder.Remove(folder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(string id)
        {
            return _context.Folder.Any(e => e._id == id);
        }



        // ================================================= FILE SERVER MANAGER METHODS ===================================================

        // READ A TXT FILE
        public static void ReadTextFile(String file)
        {
            String pathToFile = serverPath + @"\" + file + ".txt";
            List<String> lines = System.IO.File.ReadAllLines(pathToFile).ToList();
            foreach (String line in lines)
            {
                Console.WriteLine(line);
            }
        }

        //GET ALL FOLDERS
        public static void getAllFolders(String file)
        {
            String pathToFile = serverPath + file;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            foreach (String line in AllEntries)
            {
                String folderName = line;
                folderName = folderName.Replace(pathToFile + @"\", "");

                Console.WriteLine(folderName);
            }
            Console.WriteLine("\n");

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
            Directory.CreateDirectory(location + @"\" + folderName);

            String pathToFile = location;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            Console.WriteLine("\n\n");
            foreach (String line in AllEntries)
            {
                String currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(folderName))
                {
                    return false;
                }
            }

            return true;
        }


        //CREATE A NEW TXT FILE
        public static Boolean createFile(String location, String folderName)
        {
            Console.WriteLine("\n");

            String pathToFile = location + @"\" + folderName + ".txt";
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

            if (System.IO.File.Exists(location + @"\" + fileName + ".txt"))
            {
                System.IO.File.Delete(location + @"\" + fileName + ".txt");
            }
            return true;
        }

        //DELETE A FOLDER
        public static void deleteFolder(String location, String fileName)
        {
            try
            {
                var dir = new DirectoryInfo(location + @"\" + fileName);
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










    }
}
