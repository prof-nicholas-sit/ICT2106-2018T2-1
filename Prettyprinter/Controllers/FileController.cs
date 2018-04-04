using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class FileController : Controller
    {
        public FileGateway fileGateway;
        public FolderGateway folderGateway;
        public FileController(ApplicationDbContext context)
        {
            fileGateway = new FileGateway(context);
            folderGateway = new FolderGateway(context);
        }

        // GET: File
        [HttpGet]
        public ActionResult Index(string folderId, int? sortName)
        {
            if(sortName != null)
            {
                if (sortName == 1)
                {
                    ViewBag.sortName = 2;
                    return View(fileGateway.SelectAll(folderId, "ParentID").OrderBy(f => f.name));
                }
                ViewBag.sortName = 1;
                return View(fileGateway.SelectAll(folderId, "ParentID").OrderByDescending(f => f.name));
            }
            return View(fileGateway.SelectAll(folderId, "ParentID"));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string folderId, string id, string action, string actionParameter)
        {
            if (action == "rename")
            {
                folderGateway.RenameFile(id, actionParameter);
                return RedirectToAction(nameof(Index), folderId);
            }
            else if(action == "move")
            {
                folderGateway.MoveFile(id, actionParameter);
                return RedirectToAction(nameof(Index), folderId);
            }
            return NotFound();
        }

        // POST: File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string deleteId)
        {
            folderGateway.DeleteFile(deleteId);
            return RedirectToAction(nameof(Index));
        }
    }
}
