using System;
using System.Collections.Generic;
using System.Text;

namespace Prettyprinter.Models
{
    public class Metadata
    {
        private string itemId;
	    private string ownerId;	
        private string name;
        private int itemType;
        private DateTime dateTime;
        private string ancestorId;
        private string parentId;
        private List<AccessControl> accessControls;

        public Metadata(string itemId, string ownerId, string name, int itemType, DateTime dateTime, string ancestorId, string parentId, List<AccessControl> accessControls)
        {
            this.itemId = itemId;
			this.ownerId = ownerId;
            this.name = name;
            this.itemType = itemType;
            this.dateTime = dateTime;
            this.ancestorId = ancestorId;
            this.parentId = parentId;
            this.accessControls = accessControls;
        }

        public string GetItemId()
        {
            return this.itemId;
        }

        public void SetItemId(string itemId)
        {
            this.itemId = itemId;
        }

        public string GetOwnerId()
        {
            return this.ownerId;
        }

        public void SetOwnerId(string ownerId)
        {
            this.ownerId = ownerId;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetItemType()
        {
            return this.itemType;
        }

        public void SetItemType(int itemType)
        {
            this.itemType = itemType;
        }

        public DateTime GetDateTime()
        {
            return this.dateTime;
        }

        public void SetDateTime(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public string GetAncestorId()
        {
            return this.ancestorId;
        }

        public void SetAncestorId(string ancestorId)
        {
            this.ancestorId = ancestorId;
        }

        public string GetParentId()
        {
            return this.parentId;
        }

        public void SetParentId(string parentId)
        {
            this.parentId = parentId;
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
