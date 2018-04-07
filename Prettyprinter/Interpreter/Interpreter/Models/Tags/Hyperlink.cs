using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Include web linking and labeling with hyperlink markdown
    public class Hyperlink
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;

            if (s.Equals("[")) { metadata = "/hlabel*/"; }
            else if (s.Equals("]")) { metadata = "/*hlabel/"; }
            else if (s.Equals("(")) { metadata = "/hurl*/"; }
            else if (s.Equals(")")) { metadata = "/*hurl/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/hlabel*/")) { syntax = "["; }
            else if (s.Equals("/*hlabel/")) { syntax = "]"; }
            else if (s.Equals("/hurl*/")) { syntax = "("; }
            else if (s.Equals("/*hurl/")) { syntax = ")"; }

            return syntax;
        }
    }
}
