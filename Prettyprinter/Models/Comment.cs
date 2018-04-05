using System;

namespace Prettyprinter.Models
{
    public class Comment : Document
    {
        public int type { get; }

        public Comment(string id, string parentId, string name, AccessControl accessControl)
        {
            this.type = 2;
            this._id = id;
            this.ParentId = parentId;
            this.Name = name;
            this.AccessControl = accessControl;
            this.Date = DateTime.Now;
        }
    }
}
