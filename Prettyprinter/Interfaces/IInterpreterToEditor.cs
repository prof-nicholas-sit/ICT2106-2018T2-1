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
        //Markdown Interpreter will call this method to pass a document from typesetter for editing
        //An ActionResult will be returned which will be the Edit Document Page with document initialized
        ActionResult ConvertToDocument(InterpreterJob job);
    }
}
