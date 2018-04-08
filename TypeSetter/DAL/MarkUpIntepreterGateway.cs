using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeSetter.Controllers;

namespace TypeSetter.DAL
{
    public class MarkUpIntepreterGateway : iMarkupIntepreter
    {
		public MarkUpIntepreterGateway()
		{
		}

        // pass to file manager
        public InterpreterJob saveFile(InterpreterJob interpreterJob)
        {
            Builder genericController = new Builder();
            InterpreterJob someJob = new InterpreterJob();
            someJob.fileName = interpreterJob.fileName;
            someJob.flagStatus = interpreterJob.flagStatus;
            someJob.markUpContent = genericController.prepareSave(interpreterJob);
			// call function from file manager interface
			new FileManagerGateway().saveFile(someJob);

			return someJob;
        }

		public void sendToMarkup(InterpreterJob interpreterJob)
		{
			//call Markup function
		}
	}
}
