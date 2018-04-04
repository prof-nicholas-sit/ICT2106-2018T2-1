using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class MetadataController
    {
        public MetadataGateway metadataGateway;
        public AccessControlGateway accessControlGateway;
        public MetadataController(ApplicationDbContext context)
        {
            metadataGateway = new MetadataGateway(context);
            accessControlGateway = new AccessControlGateway(context);
        }
        
        public IEnumerable<Metadata> GetMetadata(string _id)
        {
            IEnumerable<Metadata> metadatas = metadataGateway.SelectAll(_id, "ParentID");
            foreach(Metadata metadata in metadatas){
                metadata.SetAccessControls(accessControlGateway.SelectAll(metadata.GetItemId(), "uID").ToList());
            }
            return metadatas;
        }

        public void AddMetadata(Metadata metadata)
        {
            metadataGateway.CreateFile(metadata);
        }
    }
}
