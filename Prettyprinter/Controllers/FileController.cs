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
        public FileGateway dataGateway;
        public FileController(ApplicationDbContext context)
        {
            dataGateway = new FileGateway(context);
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
                    return View(dataGateway.SelectAll(folderId).OrderBy(f => f.name));
                }
                ViewBag.sortName = 1;
                return View(dataGateway.SelectAll(folderId).OrderByDescending(f => f.name));
            }
            return View(dataGateway.SelectAll(folderId));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string folderId, string id, string action, string actionParameter)
        {
            if (action == "rename")
            {
                dataGateway.RenameFile(id, actionParameter);
                return RedirectToAction(nameof(Index), folderId);
            }
            else if(action == "move")
            {
                dataGateway.MoveFile(id, actionParameter);
                return RedirectToAction(nameof(Index), folderId);
            }
            return NotFound();
        }

        // POST: File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string deleteId)
        {
            dataGateway.DeleteFile(deleteId);
            return RedirectToAction(nameof(Index));
        }
    }
}
