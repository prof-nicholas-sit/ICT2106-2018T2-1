using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    /**
     *      ================================================= FILE SERVER MANAGER UTILITIES ===================================================
     * */
    public class FileStorageGateway
    {

        private static String serverPath = @"2107 File Server\";

   
        // READ A TXT FILE
        public static void readFile(String file)
        {
            String pathToFile = serverPath + @"\" + file + ".txt";
            List<String> lines = System.IO.File.ReadAllLines(pathToFile).ToList();

        }

        //GET PARENT PATH OF FILE/FOLDER
        public static String getParentOfFile(String parent)
        {
            String ParentName = Directory.GetParent(parent).ToString();
            return ParentName;
        }

        //CREATE A NEW FOLDER
        public static Boolean createFolder(String location, String folderId)
        {
            String path = serverPath + location;


            String pathToFile = path;
            List<String> AllEntries = Directory.GetDirectories(pathToFile).ToList();

            // Console.WriteLine("\n\n");
            foreach (String line in AllEntries)
            {
                String currentFolder = line;
                currentFolder = currentFolder.Replace(pathToFile + @"\", "");
                if (currentFolder.Equals(folderId))
                {

                    return false;
                }
            }
            Directory.CreateDirectory(path + @"\" + folderId);
            return true;
        }

        //CREATE A NEW TXT FILE
        public static Boolean createFile(String location, String fileId)
        {
            Console.WriteLine("\n");
            String pathToFile = serverPath + location + @"\" + fileId + ".txt";
            if (System.IO.File.Exists(pathToFile))
            {

                return false;
            }

            System.IO.File.AppendAllText(pathToFile, "");
            return true;
        }

        //DELETE A TXT FILE
        public static Boolean deleteFile(String location, String fileId)
        {

            if (System.IO.File.Exists(serverPath + location + @"\" + fileId + ".txt"))
            {
                System.IO.File.Delete(serverPath + location + @"\" + fileId + ".txt");
            }
            return true;
        }

        //DELETE A FOLDER
        public static void deleteFolder(String location, String fileId)
        {
            try
            {
                var dir = new DirectoryInfo(serverPath + location + @"\" + fileId);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //RENAMING A FOLDER
        public static Boolean renameFolder(String location, String oldName, String newName)
        {
            if (!System.IO.File.Exists(serverPath + location + @"\" + newName))
            {
                Directory.Move(serverPath + location + @"\" + oldName, serverPath + location + @"\" + newName);
                return true;
            }
            return false;
        }

        //RENAMING A TXT FILE
        public static Boolean renameFile(String location, String oldName, String newName)
        {
            if (!System.IO.File.Exists(serverPath + location + @"\" + newName + ".txt"))
            {
                System.IO.File.Move(serverPath + location + @"\" + oldName + ".txt", serverPath + location + @"\" + newName + ".txt");
                return true;
            }
            return false;
        }

        //MOVING A TXT FILE FROM ONE PATH TO ANOTHER
        public static Boolean moveFile(String location, String newLocation, String oldName)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + oldName + ".txt"))
            {
                System.IO.File.Move(serverPath + location + @"\" + oldName + ".txt", serverPath + newLocation + @"\" + oldName + ".txt");
                return true;
            }
            return false;
        }

        //COPYING A TXT FILE FROM ONE PATH TO ANTOHER
        public static Boolean copyFile(String location, String newLocation, String oldName)
        {
            if (!System.IO.File.Exists(serverPath + newLocation + @"\" + oldName + ".txt"))
            {
                System.IO.File.Copy(serverPath + location + @"\" + oldName + ".txt", serverPath + newLocation + @"\" + oldName + ".txt");
                return true;

            }
            return false;
        }
    }
}
