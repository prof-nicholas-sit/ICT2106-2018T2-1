using System;
using Prettyprinter.Models;

namespace Prettyprinter.DAL.Gateway
{
    public class CommentGateway : DataGateway<Comment>
    {
        public CommentGateway(ApplicationDbContext context) : base(context) { }
    }
}
