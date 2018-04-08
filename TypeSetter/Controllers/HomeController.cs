using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeSetter.DAL;

namespace TypeSetter.Controllers
{
    public class HomeController : Controller
    {
		
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult saveToFile()
		{
			InterpreterJob interpreterJob = new InterpreterJob
			{
				flagStatus = 0,
				markUpContent =
				"/h1*/This is a header/*h1/\n" +
				"/taJ*/A) In recent years we have all been exposed to dire media reports concerning the impending demise of global coal and oil " +
				"reserves, but the depletion of another key non-renewable resource continues without receiving much press at all. Helium – an inert," +
				" odourless, monatomic element known to lay people as the substance that makes balloons float and voices squeak when inhaled – could be " +
				"gone from this planet within a generation./*taJ/\n" +
				"/hurl*/https://xsite.singaporetech.edu.sg/?target=%2fd2l%2fhome/*hurl//hlabel*/School website/*hlabel/\n" +
				"/taC*//bs*//is*/this is nested/*ie//*be//*taC/",
				fileName = "Seriously..."
			};

			interpreterJob = new MarkUpIntepreterGateway().saveFile(interpreterJob);

			return RedirectToAction("DisplayContent", "Display",interpreterJob);
		}
	}
}
