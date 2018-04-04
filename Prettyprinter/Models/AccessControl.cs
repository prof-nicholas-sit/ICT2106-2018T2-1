using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.Models
{
    public class AccessControl
    {
        public string aID { get; set; }
        public string _id { get; set; }
        public string uID { get; set; }
        public bool MODIFY { get; set; }
        public bool COMMENT { get; set; }

        public AccessControl(string aID, string _id, string uID, bool MODIFY, bool COMMENT)
        {
            this.aID = aID;
            this._id = _id;
            this.uID = uID;
            this.MODIFY = MODIFY;
            this.COMMENT = COMMENT;
        }
    }
}
