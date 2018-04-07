using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICT2106Project.Models.Interpreter
{
    // Includes all supported emojis
    public class Emoji
    {
        public string ToTypesetter(string s)
        {
            string metadata = s;

            if (s.Equals(":smile:")) { metadata = "/*ejSmile*/"; }
            else if (s.Equals(":wink:")) { metadata = "/*ejWink*/"; }
            else if (s.Equals(":joy:")) { metadata = "/*ejJoy*/"; }
            else if (s.Equals(":fearful:")) { metadata = "/*ejFearful*/"; }
            else if (s.Equals(":grin:")) { metadata = "/*ejGrin*/"; }
            else if (s.Equals(":angry:")) { metadata = "/*ejAngry*/"; }

            return metadata;
        }

        public string ToMarkdown(string s)
        {
            string syntax = s;

            if (s.Equals("/*ejSmile*//")) { syntax = ":smile:"; }
            else if (s.Equals("/*ejWink*/")) { syntax = ":wink:"; }
            else if (s.Equals("/*ejJoy*/")) { syntax = ":joy:"; }
            else if (s.Equals("/*ejFearful*/")) { syntax = ":fearful:"; }
            else if (s.Equals("/*ejGrin*/")) { syntax = ":grin:"; }
            else if (s.Equals("/*ejAngry*/")) { syntax = ":angry:"; }

            return syntax;
        }
    }
}
