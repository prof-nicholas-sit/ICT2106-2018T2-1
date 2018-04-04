using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prettyprinter.DAL;
using Prettyprinter.Models;

// Acting as a typewriter
// String coantent -> receive from FileManager
// String path -> received from Filemanager
// Boolean newFile -> True to determine new file and false otherwise
namespace Prettyprinter.Controllers
{

    public class FileController : Controller
    {
        [Key]
        public string _id { get; set; }
        public string content { get; set; }
        public string path { get; set; }
        public Boolean newFile { get; set; }

        // Default Create File
        public FileController(string content, String path, Boolean newFile)
        {
            this.content = content;
            this.path = path;
            this.newFile = newFile;
        }

        public void setContent()
        {
            this.content = content;
        }

        public string getContent()
        {
            return content;
        }

        public void setPath(string path)
        {
            this.path = path;
        }

        public string getPath()
        {
            return path;
        }

        public void setNewFile(Boolean newFile)
        {
            this.newFile = newFile;
        }

        public Boolean getNewFile()
        {
            return newFile;
        }


    }
}
