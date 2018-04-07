using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    interface IPreHeaderMetadataBuilder
    {
        void OpenPreHeader(int destination);
        void NewHeading(string fontFamily, double fontSize, bool emojiSupport, string lastModified);
        PreHeaderMetadata GetCompletedHeader();
    }
}
