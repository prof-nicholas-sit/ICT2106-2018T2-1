using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Include Paragraphing break and Line break
    public class Spacing
    {
        public string ToTypesetter(string s)
        {
            int count = s.Count(Char.IsWhiteSpace);
            string metadata = s;

            if (count == 2) { metadata = "/*lbreak*/"; }
            else if (count > 2) { metadata = "/*pbreak*/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/*lbreak*/")) { syntax = "  "; }
            else if (s.Equals("/*pbreak*/")) { syntax = "   "; }

            return syntax;
        }
    }
}
