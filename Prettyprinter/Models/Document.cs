using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prettyprinter.Models
{
    public abstract class Document
    {
        [Key]
        public string _id { get; set; }
        public string ParentId { get; set; }
        public virtual int type { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public AccessControl AccessControl { get; set; }
        public DateTime Date { get; set; }
    }
}
