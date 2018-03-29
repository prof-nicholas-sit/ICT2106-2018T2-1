using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ThreadTest.Models.CommentModule
{
    public class Comment
    {
        [Required]
        public int ancestorId { get; set; }
        public int parentId { get; set; }
        public int id { get; set; }
        public string username { get; set; }
        public string description { get; set; }
    }
}
