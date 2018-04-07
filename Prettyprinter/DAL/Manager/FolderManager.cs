using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    public class FolderManager : DocumentManager
    {

        private static string serverPath = @"2107 File Server\";

        //Implementations of Abstract Methods
        public override string getParentOfDocument(string fileID)
        {
            string ParentName = Directory.GetParent(fileID).ToString();
            return ParentName;
        }
        
        public override bool createDocument(string path, string fileID)
        {
            string Path = path + fileID;

            string pathToFile = serverPath + path;
            List<string> AllEntries = Directory.GetDirectories(pathToFile).ToList();
            
            foreach (string line in AllEntries)
            {
                string currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(fileID))
                {
                    return false;
                }
            }   
            Directory.CreateDirectory(pathToFile + @"\" + fileID);
            return true;
        }
        
        public override bool deleteDocument(string path, string fileID)
        {
            try
            {
                var dir = new DirectoryInfo(serverPath + path + @"\" + fileID);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
