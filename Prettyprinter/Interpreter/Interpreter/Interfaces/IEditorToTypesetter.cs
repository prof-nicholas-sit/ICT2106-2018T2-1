using Interpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interfaces
{
    interface IEditorToTypesetter
    {
        InterpreterJob ConvertToTypesetter(InterpreterJob job);
    }
}
