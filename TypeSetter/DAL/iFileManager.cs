using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace TypeSetter.DAL
{
    interface iFileManager
    {
		void saveFile(InterpreterJob interpreterJob);
		void openFile(string fileName);
    }
}
