using System;

namespace Prettyprinter.Models
{
    public class File : Document
    {
        public int type { get; }

        public File() { }

        public File(string id, string parentId, string name, AccessControl accessControl)
        {
            this.type = 1;
            this._id = id;
            this.ParentId = parentId;
            this.Name = name;
            this.AccessControl = accessControl;
            this.Date = DateTime.Now;
        }
    }
}
