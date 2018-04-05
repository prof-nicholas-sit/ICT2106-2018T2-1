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
        public string name { get; set; }
        public string content { get; set; }
        public string parentId { get; set; }
        public Boolean newFile { get; set; }

        // Default Create File
        public FileController()
        {
        }

        public string getId()
        {
            return _id;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public void setContent(string content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return content;
        }

        public void setParentId(string path)
        {
            this.parentId = path;
        }

        public string getParentId()
        {
            return parentId;
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
