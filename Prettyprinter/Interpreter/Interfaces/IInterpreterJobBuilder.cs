using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    interface IInterpreterJobBuilder
    {
        void OpenJob(int destination, string fileName);
        void BuildPreHeaderMetadata(string fontFamily, double fontSize, bool emojiSupport, string lastModified);
        void BuildContent(string content);
        void CloseJob();
        InterpreterJob GetCompletedJob();
    }
}
