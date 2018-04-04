using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.Models
{
    public class MetaData
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string type { get; set; }
        public List<AccessControl> accessControl { get; set; }
        public DateTime date { get; set; }

        public MetaData(string _id, string name, string parentId, string type, List<AccessControl> accessControl, DateTime date)
        {
            this._id = _id;
            this.name = name;
            this.parentId = parentId;
            this.type = type;
            this.accessControl = accessControl;
            this.date = date;
        }
    }
}
