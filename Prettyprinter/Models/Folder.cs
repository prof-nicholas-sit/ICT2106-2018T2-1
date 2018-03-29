using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.Models
{
    public class Folder
    {
        [Key]
        public string _id { get; set; }
        public string parentId { get; set; }
        public string type { get; set; }

        public string name { get; set; }
        [NotMapped]
        public string[] accessControl { get; set; }
        public DateTime date { get; set; }
    }
}
