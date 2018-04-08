using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypeSetter
{
    public class HTML
    {
        string header = "";
        string body = "";
        string footer = "";

        public HTML(string header, string body, string footer)
        {
            this.header = header;
            this.body = body;
            this.footer = footer;
        }
    }
}
