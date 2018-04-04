using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class AccessController
    {
        public List<Metadata> getMetaData(string path)
        {
            List<Metadata> result = new List<Metadata>();

            //result.Add(new Metadata("id1", "file1", "parentId1", "file", new List<AccessControl>(), new DateTime()));
            //result.Add(new Metadata("id1", "folder1", "parentId1", "folder", new List<AccessControl>(), new DateTime()));
            //result.Add(new Metadata("id1", "file2", "parentId1", "file", new List<AccessControl>(), new DateTime()));

            return result;
        }

        public void createMetaData(Metadata metaData)
        {
            System.Diagnostics.Debug.WriteLine("Create MetaData DONE !");
        }
    }
}
