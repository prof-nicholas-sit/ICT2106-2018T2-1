using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prettyprinter.Models
{
    public class MetadataModel
    {
        [BsonId]
        [BsonElement(elementName:"_id")]
        public ObjectId itemId { get; set; }
        [BsonElement(elementName:"OwnerID")]      
        public string ownerId { get; set; }
        [BsonElement(elementName:"Name")]
        public string name { get; set; }
        [BsonElement(elementName:"ItemType")]
        public int itemType { get; set; }
        [BsonElement(elementName:"DateTime")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime dateTime { get; set; }
        [BsonElement(elementName:"AncestorID")]
        public string ancestorId { get; set; }
        [BsonElement(elementName:"ParentID")]
        public string parentId { get; set; }
        [BsonElement(elementName:"AccessControl")]
        public List<AccessControlsModel> accessControls { get; set; }
        
        public MetadataModel(ObjectId itemId, string ownerId, string name, int itemType, DateTime dateTime, string ancestorId, string parentId, List<AccessControlsModel> accessControls)
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
    }
}