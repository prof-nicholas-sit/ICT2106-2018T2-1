using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Includes Left, Center, Right, Centre Alignment
    public class Alignment
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;
            //End Tag
            if (s.Contains("/"))
            {
                if (s.Equals("</-")) { metadata = "/*taL/"; }
                else if (s.Equals("-/>")) { metadata = "/*taR/"; }
                else if (s.Equals("<-/>")) { metadata = "/*taC/"; }
                else if (s.Equals("<--/>")) { metadata = "/*taJ/"; }
            }
            else
            {
                if (s.Equals("<-")) { metadata = "/taL*/"; }
                else if (s.Equals("->")) { metadata = "/taR*/"; }
                else if (s.Equals("<->")) { metadata = "/taC*/"; }
                else if (s.Equals("<-->")) { metadata = "/taJ*/"; }

            }
            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;
            //End Tag
            if (s.Contains("/*"))
            {
                if (s.Equals("/*taL/")) { syntax = "</-"; }
                else if (s.Equals("/*taR/")) { syntax = "-/>"; }
                else if (s.Equals("/*taC/")) { syntax = "<-/>"; }
                else if (s.Equals("/*taJ/")) { syntax = "<--/>"; }
            }
            else
            {
                if (s.Equals("/taL*/")) { syntax = "<-"; }
                else if (s.Equals("/taR*/")) { syntax = "->"; }
                else if (s.Equals("/taC*/")) { syntax = "<->"; }
                else if (s.Equals("/taJ*/")) { syntax = "<-->"; }

            }
            return syntax;
        }
    }
}
