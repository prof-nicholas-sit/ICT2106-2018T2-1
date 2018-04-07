using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{

    public class CommentManager : DocumentManager
    {
        private static String serverPath = @"2107 File Server\Comment\";

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

        //CommentManager's Method
        public String readDocument(String fileID)
        {
            String pathToFile = serverPath + @"\" + fileID + ".txt";
            String lines = System.IO.File.ReadAllLines(pathToFile).ToString();
            return lines;
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
