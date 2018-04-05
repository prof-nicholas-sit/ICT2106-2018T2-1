using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Include Ordered and Unordered List
    public class List
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;

            if (s.Equals("+")) { metadata = "/ul*/"; }
            else if (s.Equals("+`")) { metadata = "/*ul/"; }
            else if (s.Equals("++")) { metadata = "/usb*/"; }
            else if (s.Equals("++`")) { metadata = "/*usb/"; }
            else if (s.Equals("%")) { metadata = "/ol*/"; }
            else if (s.Equals("%`")) { metadata = "/*ol/"; }
            else if (s.Equals("%%")) { metadata = "/osl*/"; }
            else if (s.Equals("%%`")) { metadata = "/*osl/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/ul*/")) { syntax = "+"; }
            else if (s.Equals("/*ul/")) { syntax = "+`"; }
            else if (s.Equals("/usb*/")) { syntax = "++"; }
            else if (s.Equals("/*usb/")) { syntax = "++`"; }
            else if (s.Equals("/ol*/")) { syntax = "%"; }
            else if (s.Equals("/*ol/")) { syntax = "%`"; }
            else if (s.Equals("/osl*/")) { syntax = "%%"; }
            else if (s.Equals("/*osl/")) { syntax = "%%`"; }

            return syntax;
        }
    }
}
