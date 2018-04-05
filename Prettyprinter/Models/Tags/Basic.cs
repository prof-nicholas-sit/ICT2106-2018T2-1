using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Includes Bold, Italic, Underline and Strikethrough
    public class Basic
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;

            if (s.Equals("~~")) { metadata = "/is*/"; }
            else if (s.Equals("~~`")) { metadata = "/*ie/"; }
            else if (s.Equals("__")) { metadata = "/us*/"; }
            else if (s.Equals("__`")) { metadata = "/*ue/"; }
            else if (s.Equals("**")) { metadata = "/bs*/"; }
            else if (s.Equals("**`")) { metadata = "/*be/"; }
            else if (s.Equals("--")) { metadata = "/ss*/"; }
            else if (s.Equals("--`")) { metadata = "/*se/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/is*/")) { syntax = "~~"; }
            else if (s.Equals("/*ie/")) { syntax = "~~`"; }
            else if (s.Equals("/us*/")) { syntax = "__"; }
            else if (s.Equals("/*ue/")) { syntax = "__`"; }
            else if (s.Equals("/bs*/")) { syntax = "**"; }
            else if (s.Equals("/*be/")) { syntax = "**`"; }
            else if (s.Equals("/ss*/")) { syntax = "--"; }
            else if (s.Equals("/*se/")) { syntax = "--`"; }

            return syntax;
        }
    }
}
