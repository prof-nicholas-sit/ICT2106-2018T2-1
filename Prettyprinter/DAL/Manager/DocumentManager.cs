using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    public abstract class DocumentManager
    {
        public abstract string getParentOfDocument(string fileID);
        public abstract bool createDocument(string path, string fileID);
        public abstract bool deleteDocument(string path, string fileID);
    }
}
