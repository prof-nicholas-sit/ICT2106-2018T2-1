using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypeSetter.HTMLBuilder
{
    interface iBuilder
    {
		void preparePreview(InterpreterJob interpreterJob);

		//Function to save file
		string prepareSave(InterpreterJob interpreter);

		//Function to send back to Markup
		void prepareForMarkup(InterpreterJob interpreter);
		
	}
}
