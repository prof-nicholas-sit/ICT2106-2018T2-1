using Interpreter.Controllers;
using Interpreter.Interfaces;
using Interpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Editor.Interfaces;

namespace Editor.Controllers
{
    public class DocumentController : Controller , IInterpreterToEditor
    {
        static InterpreterJob currentJob;
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        // GET: Document/Details/
        //This method will show the details of a document after it is processed by
        //markdown interpreter and it will include the preview 
        public ActionResult Details()
        {
            InterpreterJob content = currentJob;
            IEditorToTypesetter eot = new JobController();
            InterpreterJob resultingJobForTypesetter = eot.ConvertToTypesetter(content);
            return View(resultingJobForTypesetter);
        }

        // GET: Document/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InterpreterJob job)
        {
            try
            {
                // When a document is created, it will be strongly typed into a InterpreterJob object
                // The destination flag is set so that interpreter knows this should be passed to the typesetter
                // modifiedFlag is 0 because this is a new document and the current time will be the LastModified date
                // It will redirect to details page for users to preview information
                job.DestinationFlag = 1;
                job.modifiedFlag = 0;
                job.LastModified = DateTime.UtcNow.Date.ToString("d");
                currentJob= job;
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Edit/5
        public ActionResult Edit()
        {
            return View(currentJob);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(InterpreterJob job)
        {
            try
            {
                // When a document is edited, it will be strongly typed into a InterpreterJob object
                // The destination flag is set so that interpreter knows this should be passed to the typesetter
                // modifiedFlag is 1 because this is a existing document and the current time will be the LastModified date
                // It will redirect to details page for users to preview information
                job.DestinationFlag = 1;
                job.modifiedFlag = 1;
                job.LastModified = DateTime.UtcNow.Date.ToString("d");
                currentJob = job;
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(InterpreterJob job)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Markdown Interpreter to Document Editor for editing of documents
        //currentJob will be initialized to this and redirected to edit page for editing
        public ActionResult ConvertToDocument(InterpreterJob job)
        {
            currentJob = job;
            return RedirectToAction("Edit");
        }
    }
}
