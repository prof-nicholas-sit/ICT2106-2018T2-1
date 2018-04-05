using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Prettyprinter.Models
{
    public class Metadata
    {
        [Key]
        public string _id { get; set; }

        public string Creator { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public string AncestorID { get; set; }
        public string ParentID { get; set; }
        public List<AccessControl> accessControls;

        public Metadata(string _id, string Creator, string Name, int Type, string AncestorID, string ParentID, List<AccessControl> accessControls)
        {
            this._id = _id;
			this.Creator = Creator;
            this.Name = Name;
            this.Type = Type;
            this.Date = DateTime.Now;
            this.AncestorID = AncestorID;
            this.ParentID = ParentID;
            this.accessControls = accessControls;
        }

        public string GetItemId()
        {
            return this._id;
        }

        public void SetItemId(string itemId)
        {
            this._id = itemId;
        }

        public string GetOwnerId()
        {
            return this.Creator;
        }

        public void SetOwnerId(string ownerId)
        {
            this.Creator = ownerId;
        }

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public int GetItemType()
        {
            return this.Type;
        }

        public void SetItemType(int itemType)
        {
            this.Type = itemType;
        }

        public DateTime GetDateTime()
        {
            return this.Date;
        }

        public void SetDateTime(DateTime dateTime)
        {
            this.Date = dateTime;
        }

        public string GetAncestorId()
        {
            return this.AncestorID;
        }

        public void SetAncestorId(string ancestorId)
        {
            this.AncestorID = ancestorId;
        }

        public string GetParentId()
        {
            return this.ParentID;
        }

        public void SetParentId(string parentId)
        {
            this.ParentID = parentId;
        }

        public List<AccessControl> GetAccessControls()
        {
            return this.accessControls;
        }

        public void SetAccessControls(List<AccessControl> accessControls)
        {
            this.accessControls = accessControls;
        }
    }
}
