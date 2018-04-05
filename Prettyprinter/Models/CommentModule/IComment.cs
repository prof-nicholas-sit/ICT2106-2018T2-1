using System;
namespace Prettyprinter.Models.CommentModule
{
    public interface IComment
    {

        // get a string containing the formatted document
        string GetString();
    }
}
