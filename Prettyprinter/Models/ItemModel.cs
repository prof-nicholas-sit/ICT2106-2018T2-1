using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DBLayer
{
    [BsonIgnoreExtraElements]
    public class ItemModel
    {
        /*
         * _id
         *  name
         *  parent
         *  type
         *  accessControl
         *  date
         *  AncestorID
         *  ParentID
         */
        [BsonId]
        [BsonElement(elementName:"_id")]
        public ObjectId ID { get; set; }
        [BsonElement(elementName:"Name")]
        private string Name { get; set; }
        [BsonElement(elementName:"Parent")]
        private string Parent { get; set; }
        [BsonElement(elementName:"Type")]
        [BsonRepresentation(BsonType.Int64)]
        private int Type { get; set; }
        [BsonElement(elementName:"Date")]
        [BsonRepresentation(BsonType.DateTime)]
        private DateTime Date { get; set; }
        [BsonElement(elementName:"AncestorID")]
        private string AncestorId { get; set; }
        [BsonElement(elementName:"ParentID")]
        private string ParentId { get; set; }
        [BsonElement(elementName:"AccessControl")]
        //[BsonRepresentation(BsonType.Array)]
        private BsonArray Access { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        
        internal ItemModel(ObjectId id, string name, string parent, int type, DateTime date, string ancestorId, string parentId, BsonArray access)
        {
            ID = id;
            Name = name;
            Parent = parent;
            this.Type = type;
            this.Date = date;
            AncestorId = ancestorId;
            ParentId = parentId;
            this.Access = access;
        }
    }
}