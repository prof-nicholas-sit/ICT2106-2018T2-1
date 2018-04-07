using MongoDB.Bson.Serialization.Attributes;

namespace Prettyprinter.Models
{
    public class AccessControlsModel
    {
        [BsonId]
        [BsonElement(elementName:"uID")]
        private string uID;
        [BsonElement(elementName:"COMMENT")]
        private bool comment;
        [BsonElement(elementName:"MODIFY")]
        private bool modify;

        public AccessControlsModel(string userId, bool commentAllowed, bool modifyAllowed)
        {
            
            this.uID = userId;
            this.comment = commentAllowed;
            this.modify = modifyAllowed;
        }

        public string GetUserId()
        {
            return this.uID;
        }

        public void SetUserId(string userId)
        {
            this.uID = userId;
        }

        public bool IsCommentAllowed()
        {
            return this.comment;
        }

        public void SetCommentAllowed(bool commentAllowed)
        {
            this.comment = commentAllowed;
        }

        public bool IsModifyAllowed()
        {
            return this.modify;
        }

        public void SetModifyAllowed(bool modifyAllowed)
        {
            this.modify = modifyAllowed;
        }
    }
}