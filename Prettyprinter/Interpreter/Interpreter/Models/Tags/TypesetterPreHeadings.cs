using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models.Tags
{
    class TypesetterPreHeadings
    {
        public string GetStart()
        {
            return "/*mdStart*/";
        }

        public string GetFont(string fontFamily)
        {
            return "/mdfont*/" + fontFamily + "/*mdfont/";
        }

        public string GetFontSize(double fontSize)
        {
            return "/mdfs*/" + fontSize.ToString() + "/*mdfs/";
        }

        public string GetEmojiSupport(bool isSupported)
        {
            return "/mdes*/" + isSupported.ToString() + "/*mdes/";
        }

        public string GetLastModified(string date)
        {
            return "/mdlm*/" + date + "/*mdlm/";
        }

        public string GetEnd()
        {
            return "/*mdEnd*/";
        }
    }
}
