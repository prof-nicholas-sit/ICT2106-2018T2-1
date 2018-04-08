using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypeSetter.DAL
{
    interface iMarkupIntepreter
    {
        void sendToMarkup(InterpreterJob interpreterJob);
        InterpreterJob saveFile(InterpreterJob interpreterJob);
    }
}
