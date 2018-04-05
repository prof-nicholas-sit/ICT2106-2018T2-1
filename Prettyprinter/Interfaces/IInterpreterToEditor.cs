using Interpreter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Editor.Interfaces
{
    interface IInterpreterToEditor
    {
        ActionResult ConvertToDocument(InterpreterJob job);
    }
}
