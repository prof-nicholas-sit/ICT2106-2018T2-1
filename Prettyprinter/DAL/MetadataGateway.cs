using System.Collections.Generic;
using MongoDB.Bson;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class MetadataGateway : DataGateway<MetadataModel>
    {
        private static MetadataGateway _instance;
        
        private MetadataGateway(DbObj dbObject) : base(dbObject)
        {
        }

        public static MetadataGateway GetInstance(DbObj dbObject)
        {
            if (_instance == null)
            {
                _instance = new MetadataGateway(dbObject);
            }

            return _instance;
        }

        public IEnumerable<MetadataModel> GetChildren(ObjectId objId)
        {
            var resultInArray = db.SelectWhere(typeof(MetadataModel), "ParentID", objId.ToString());
            return (IEnumerable<MetadataModel>)resultInArray;
        }
        
        public IEnumerable<MetadataModel> GetChildrenById(string objID)
        {
            MetadataModel rootFolder = null;
            var resultInArray = db.SelectWhere(typeof(MetadataModel), "ParentID", objID);
            return (IEnumerable<MetadataModel>)resultInArray;
        }
        
        public IEnumerable<MetadataModel> GetChildren(string email)
        {
            MetadataModel rootFolder = null;
            var resultInArray = db.SelectWhere(typeof(MetadataModel), "Name", email).GetEnumerator();
            // need to get the first element of the returned enumerable
            while (resultInArray.MoveNext())
            {
                rootFolder = (MetadataModel)resultInArray.Current;
                if (string.IsNullOrEmpty(rootFolder?.parentId))
                {
                    break;
                }
            }
            
            // return children of root folder
            return rootFolder != null ? GetChildren(rootFolder.itemId) : null;
        }
        
        public IEnumerable<MetadataModel> GetComments(ObjectId objId)
        {
            var resultInArray = db.SelectWhere(typeof(MetadataModel), "AncestorId", objId.ToString());
            return (IEnumerable<MetadataModel>)resultInArray;
        }
        
    }
}