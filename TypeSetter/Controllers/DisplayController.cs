using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeSetter.DAL;
using TypeSetter.Models;

namespace TypeSetter.Controllers

{
    public class DisplayController : Controller
    {
		InterpreterJob interpreter;

		//Display Content
		public ActionResult DisplayContent(InterpreterJob interpreterJob)
		{
			interpreter = interpreterJob;
			return View("~/Views/Display/DisplayContent.cshtml", interpreter.markUpContent);
		}

		public void returnToMarkup()
		{
			new Builder().prepareForMarkup(this.interpreter);
		}
	}
}
