using Interpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interfaces
{
    interface IContentBuilder
    {
        void OpenContentBuilder(int destination, string content);
        void ConvertContent();
        Content GetConvertedContent();
    }
}
