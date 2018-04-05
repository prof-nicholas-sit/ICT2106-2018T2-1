using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    public class FileManager : DocumentManager
    {
        //Implementations of Abstract Methods
        public override String readFile(String fileID)
        {
            return "";
        }
        public override String getParentOfFile(String fileID)
        {
            return "";
        }
        public override Boolean createDocument(String path, String fileID)
        {
            return false;
        }
        public override Boolean deleteDocument(String path, String fileID)
        {
            return false;
        }

        // FileManager-Only Method
        public Boolean copyDocument(String source, String destination, String fileID)
        {
            return false;
        }
        public Boolean moveDocument(String source, String destination, String fileID)
        {
            return false;
        }
    }
}
