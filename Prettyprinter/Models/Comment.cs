using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.Models
{
    public class Comment
    {
        [Required]
        public int _id { get; set; }
        public int parentId { get; set; }

        public int ancestorId { get; set; }
        public string username { get; set; }
        public string description { get; set; }
    }
}
