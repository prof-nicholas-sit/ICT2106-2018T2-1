using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypeSetter.DAL
{
    public class DACGateway : iDAC
    {

        public bool checkPermission(string FileName, string Action)
        {
			//Call function from Data access
			//Pass file name and action(edit) as parameters
			//true == allowed to edit, false == not allowed.
			return true;
        }
    }
}
