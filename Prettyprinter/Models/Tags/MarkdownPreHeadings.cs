using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models.Tags
{
    class MarkdownPreHeadings
    {
        public string GetStart()
        {
            return "mdStart";
        }

        public string GetFont(string fontFamily)
        {
            return "mdFont='" + fontFamily + "'";
        }

        public string GetFontSize(double fontSize)
        {
            return "mdFontSize='" + fontSize.ToString() + "'";
        }

        public string GetEmojiSupport(bool isSupported)
        {
            return "mdEmoji='" + isSupported.ToString() + "'";
        }

        public string GetLastModified(string date)
        {
            return "mdLastModified='" + date + "'";
        }

        public string GetEnd()
        {
            return "mdEnd";
        }
    }
}
