using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DBLayer
{
    [BsonIgnoreExtraElements]
    public class AccessControlModel
    {
        [BsonId]
        [BsonElement(elementName:"_id")]
        private ObjectId ID { get; set; }
        [BsonElement(elementName:"uID")]
        private string uID { get; set; }
        [BsonElement(elementName:"MODIFY")]
        [BsonRepresentation(BsonType.Boolean)]
        private Boolean Modify { get; set; }
        [BsonElement(elementName:"COMMENT")]
        [BsonRepresentation(BsonType.Boolean)]
        private Boolean Comment { get; set; }

        internal AccessControlModel(ObjectId id, string uId, bool modify, bool comment)
        {
            ID = id;
            uID = uId;
            Modify = modify;
            Comment = comment;
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}