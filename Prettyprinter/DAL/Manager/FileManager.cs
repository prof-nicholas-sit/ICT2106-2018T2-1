using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    public class FileManager : DocumentManager
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
            string pathToFile = serverPath + path + @"\" + fileID + ".txt";
            if (System.IO.File.Exists(pathToFile))
            {

                return false;
            }
            System.IO.File.AppendAllText(pathToFile, "");
            return true;
        }
        
        public override bool deleteDocument(string path, string fileID)
        {
            if (System.IO.File.Exists(serverPath + path + @"\" + fileID + ".txt"))
            {
                System.IO.File.Delete(serverPath + path + @"\" + fileID + ".txt");
            }
            return true;
        }
        
        // FileManager's Method
        public string readDocument(string fileID)
        {
            string pathToFile = serverPath + @"\" + fileID + ".txt";
            string lines = System.IO.File.ReadAllLines(pathToFile).ToString();
            return lines;
        }

        public bool copyDocument(string source, string destination, string fileID)
        {
            if (System.IO.File.Exists(serverPath + source + @"\" + fileID + ".txt"))
            {
                System.IO.File.Copy(serverPath + source + @"\" + fileID + ".txt", serverPath + destination + @"\" + fileID + ".txt");
                return true;

            }
            return false;
        }
        
        public bool moveDocument(string source, string destination, string fileID)
        {
            if (!System.IO.File.Exists(serverPath + source + @"\" + fileID + ".txt"))
            {
                System.IO.File.Move(serverPath + source + @"\" + fileID + ".txt", serverPath + destination + @"\" + fileID + ".txt");
                return true;
            }
            return false;
        }

        public void writeDocument(string path, string content, string fileId, bool doReplace)
        {
            if (File.Exists(serverPath + path + @"\" + fileId + ".txt"))
            {
                if (doReplace == false)
                {
                    File.AppendAllText(serverPath + path + @"\" + fileId + ".txt", content + Environment.NewLine);
                }
                else
                {
                    File.WriteAllText(serverPath + path + @"\" + fileId + ".txt", content + Environment.NewLine);
                }
            }
        }
    }
}
