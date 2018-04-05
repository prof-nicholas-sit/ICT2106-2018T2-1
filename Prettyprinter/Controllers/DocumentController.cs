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

namespace Editor.Controllers
{
    public class DocumentController : Controller
    {
        static InterpreterJob currentJob;
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        // GET: Document/Details/5
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
                // TODO: Add insert logic here
                job.DestinationFlag = 1;
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(InterpreterJob job)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
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
    }
}
