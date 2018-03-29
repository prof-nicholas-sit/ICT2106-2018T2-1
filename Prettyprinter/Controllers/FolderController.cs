using System;
using System.Collections.Generic;
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
        public FolderController(ApplicationDbContext context)
        {
            folderGateway = new FolderGateway(context);
        }

        // GET: Folder
        public ActionResult Index()
        {
            //System.Diagnostics.Debug.WriteLine("*** HAHA"+ HttpContext.Request.Path.ToString(), "HAHA");
            if (HttpContext.Session.GetString("Path") == null)
            {
                HttpContext.Session.SetString("Path", "root");
            }
            var path = HttpContext.Session.GetString("Path");
            ViewBag.Path = path;

            IEnumerable<Folder> a = folderGateway.SelectAll(path);
            System.Diagnostics.Debug.WriteLine("TOTAL OF FOLDER FOUND : "+a.Count()+" WITH PARENTID : "+path);

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

            return RedirectToAction(nameof(Index));
        }

        // POST: Folder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            folderGateway.DeleteFile(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(string id)
        {
            return (folderGateway.SelectById(id) != null);
        }
    }
}
