
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeSetter.DAL;
using TypeSetter.HTMLBuilder;
using TypeSetter.Models;

namespace TypeSetter.Controllers
{
	public class Builder : iBuilder
	{
		EditController editController = new EditController();

		//Function to Preview file
		public void preparePreview(InterpreterJob interpreterJob)
		{
			interpreterJob.markUpContent = editController.ReverseConvert(interpreterJob.markUpContent);
			new DisplayController().DisplayContent(interpreterJob);
		}

		//Function to save file
		public string prepareSave(InterpreterJob interpreter)
		{
			interpreter.markUpContent = editController.Convert(interpreter);

			return interpreter.markUpContent;
		}

		//Function to send back to Markup
		public void prepareForMarkup(InterpreterJob interpreter)
		{
			new MarkUpIntepreterGateway().sendToMarkup(interpreter);
		}
	}
}
