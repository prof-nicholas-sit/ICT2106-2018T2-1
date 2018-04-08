using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using TypeSetter.DAL;
using TypeSetter.Models;

namespace TypeSetter.Controllers
{
	public class EditController : Controller
	{

		public EditController() {
			//empty constructor
		}

		//content for editing is send to MarkUpInterpreter
		public string Convert(InterpreterJob interpreterjob)
		{
			int flag = interpreterjob.flagStatus;
			string content = interpreterjob.markUpContent;

			if (flag == 1)
			{
				return "ERROR";
			}
			else
			{
				ErrorCheckingModel errorCheckingModel = new ErrorCheckingModel();
				int errorCheckingFlag = errorCheckingModel.CheckErrorInContent(content);

				
				if (errorCheckingFlag == 1)
				{
					return "ERROR in tags";
				}
				else
				{
					LevelCheckingModel levelCheckingModel = new LevelCheckingModel();
					string leveledContent = levelCheckingModel.CheckLevelInStrings(content);

					ConvertModel convertModel = new ConvertModel();
					string convertedContent = convertModel.convertToHTML(leveledContent);

					return convertedContent;
				}
			}
		}

		public string ReverseConvert(string content)
		{
			ReverseConvertModel reverseConvertModel = new ReverseConvertModel();
			string reverseConvertedContent = reverseConvertModel.convertBackToMarkUp(content);
			return reverseConvertedContent;

		}

	}
}
