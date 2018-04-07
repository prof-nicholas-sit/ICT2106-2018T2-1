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

        private static String serverPath = @"2107 File Server\";

        //Stub constant to represent getting userID from Session
        private static String currentUserID = "161616";
        
        //Implementations of Abstract Methods
        public override String getParentOfDocument(String fileID)
        {
            String ParentName = Directory.GetParent(fileID).ToString();
            return ParentName;

        }
        
        public override Boolean createDocument(String path, String fileID)
        {
            String pathToFile = serverPath + path + @"\" + fileID + ".txt";
            if (System.IO.File.Exists(pathToFile))
            {

                return false;
            }
            System.IO.File.AppendAllText(pathToFile, "");
            return true;
        }
        
        public override Boolean deleteDocument(String path, String fileID)
        {
            if (System.IO.File.Exists(serverPath + path + @"\" + fileID + ".txt"))
            {
                System.IO.File.Delete(serverPath + path + @"\" + fileID + ".txt");
            }
            return true;
        }
        
        // FileManager's Method
        public String readDocument(String fileID)
        {
            String pathToFile = serverPath + @"\" + fileID + ".txt";
            String lines = System.IO.File.ReadAllLines(pathToFile).ToString();
            return lines;
        }

        public Boolean copyDocument(String source, String destination, String fileID)
        {
            if (System.IO.File.Exists(serverPath + source + @"\" + fileID + ".txt"))
            {
                System.IO.File.Copy(serverPath + source + @"\" + fileID + ".txt", serverPath + destination + @"\" + fileID + ".txt");
                return true;

            }
            return false;
        }
        
        public Boolean moveDocument(String source, String destination, String fileID)
        {
            if (!System.IO.File.Exists(serverPath + source + @"\" + fileID + ".txt"))
            {
                System.IO.File.Move(serverPath + source + @"\" + fileID + ".txt", serverPath + destination + @"\" + fileID + ".txt");
                return true;
            }
            return false;
        }

        public void writeDocument(String path, String content, String fileId, Boolean doReplace)
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
