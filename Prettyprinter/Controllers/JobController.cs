using Editor.Controllers;
using Editor.Interfaces;
using Interpreter.Interfaces;
using Interpreter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Controllers
{
    class JobController : IEditorToTypesetter, ITypesetterToEditor
    {
        public InterpreterJob ConvertToEditor(InterpreterJob job)
        {
            InterpreterJobBuilder builder = new InterpreterJobBuilder();
            builder.OpenJob(job.DestinationFlag, job.FileName);
            builder.BuildPreHeaderMetadata(job.FontFamily, job.FontSize, job.EmojiSupport, job.LastModified);
            builder.BuildContent(job.Content);
            IInterpreterToEditor ioe = new DocumentController();
            ActionResult editPage = ioe.ConvertToDocument(builder.GetCompletedJob());
            return builder.GetCompletedJob();
        }

        public InterpreterJob ConvertToTypesetter(InterpreterJob job)
        {
            InterpreterJobBuilder builder = new InterpreterJobBuilder();
            builder.OpenJob(job.DestinationFlag, job.FileName);
            builder.BuildPreHeaderMetadata(job.FontFamily, job.FontSize, job.EmojiSupport, job.LastModified);
            builder.BuildContent(job.Content);

            return builder.GetCompletedJob();
        }
    }
}
