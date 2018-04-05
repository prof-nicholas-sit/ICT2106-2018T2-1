using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Include Text Coloring, Text Highlighting
    public class Advance
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;
            int wordCount = s.Length;

            if (s.Equals("@@`")) { metadata = "/*fc/"; }
            else if (wordCount > 2 && s.Contains("@@")) {
                string strColor = s.Remove(0, 2);
                if (strColor.Length > 0) { metadata = "/fc_#" + strColor + "*/"; }
            } else if (s.Equals("==")) { metadata = "/th*/"; }
            else if (s.Equals("==`")) { metadata = "/*th/"; }
            else if (s.Equals(">")) { metadata = "/bq*/"; }
            else if (s.Equals(">`")) { metadata = "/*bq/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;
            int wordCount = s.Length;

            if (s.Equals("/*fc/")) { syntax = " @@`"; }
            else if (s.Contains("/fc_#") && s.Contains("*/") && wordCount > 6)
            {
                //Remove first 5 string & last 2 string
                string strColor = s.Substring(5, wordCount - 7);
                if (strColor.Length > 0)
                {
                    syntax = "@@" + strColor;
                }
            } else if (s.Equals("/th*/")) { syntax = "=="; }
            else if (s.Equals("/*th/")) { syntax = "==`"; }
            else if (s.Equals("/bq*/")) { syntax = ">"; }
            else if (s.Equals("/*bq/")) { syntax = ">`"; }

            return syntax;
        }
    }
}
