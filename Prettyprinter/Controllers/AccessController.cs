using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class AccessController
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string type { get; set; }
        public string[] accessControl { get; set; }
        public DateTime date { get; set; }

        public List<MetaData> getMetaData(string path)
        {
            List<MetaData> result = new List<MetaData>();

            result.Add(new MetaData("id1", "file1", "parentId1", "file", new string[] { "email1", "true", "true" }, new DateTime()));
            result.Add(new MetaData("id1", "folder1", "parentId1", "folder", new string[] { "email2", "true", "true" }, new DateTime()));
            result.Add(new MetaData("id1", "file2", "parentId1", "file", new string[] { "email3", "true", "true" }, new DateTime()));

            return result;
        }
    }
}
