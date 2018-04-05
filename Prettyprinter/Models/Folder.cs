using System;

namespace Prettyprinter.Models
{
    public class Folder : Document
    {
        public int type { get; set; }

        public Folder() { }

        public Folder(string id, string parentId, string name, AccessControl accessControl)
        {
            this.type = 0;
            this._id = id;
            this.ParentId = parentId;
            this.Name = name;
            this.AccessControl = accessControl;
            this.Date = DateTime.Now;
        }
    }
    
}
