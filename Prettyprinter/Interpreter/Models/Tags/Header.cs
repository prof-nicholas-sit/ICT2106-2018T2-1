using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ICT2106Project.Models.Interpreter
{
    // Includes Header H1, H2, H3, H4, H5, H6, ALT-H1, ALT-H2
    public class Header
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;

            if (s.Equals("#")) { metadata = "/h1*/"; }
            else if (s.Equals("##")) { metadata = "/h2*/"; }
            else if (s.Equals("###")) { metadata = "/h3*/"; }
            else if (s.Equals("####")) { metadata = "/h4*/"; }
            else if (s.Equals("#####")) { metadata = "/h5*/"; }
            else if (s.Equals("######")) { metadata = "/h6*/"; }
            else if (s.Equals("#`")) { metadata = "/*h1/"; }
            else if (s.Equals("##`")) { metadata = "/*h2/"; }
            else if (s.Equals("###`")) { metadata = "/*h3/"; }
            else if (s.Equals("####`")) { metadata = "/*h4/"; }
            else if (s.Equals("#####`")) { metadata = "/*h5/"; }
            else if (s.Equals("######`")) { metadata = "/*h6/"; }
            else if (s.Contains("!!!")) { metadata = "/ah1*/ /*ah1/"; }
            else if (s.Contains("&&&")) { metadata = "/ah2*/ /*ah2/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/h1*/")) { syntax = "#"; }
            else if (s.Equals("/h2*/")) { syntax = "##"; }
            else if (s.Equals("/h3*/")) { syntax = "###"; }
            else if (s.Equals("/h4*/")) { syntax = "####"; }
            else if (s.Equals("/h5*/")) { syntax = "#####"; }
            else if (s.Equals("/h6*/")) { syntax = "######"; }
            else if (s.Equals("*/h1/")) { syntax = "#'"; }
            else if (s.Equals("*/h2/")) { syntax = "##'"; }
            else if (s.Equals("*/h3/")) { syntax = "###'"; }
            else if (s.Equals("*/h4/")) { syntax = "####'"; }
            else if (s.Equals("*/h5/")) { syntax = "#####'"; }
            else if (s.Equals("*/h6/")) { syntax = "######'"; }
            else if (s.Equals("/ah1*/ /*ah1/")) { syntax = "!!!"; }
            else if (s.Equals("/ah2*/ /*ah2/")) { syntax = "&&&"; }

            return syntax;
        }
    }
}
