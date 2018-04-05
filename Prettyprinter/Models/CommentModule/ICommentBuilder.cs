using System;
namespace Prettyprinter.Models.CommentModule
{
    public interface ICommentBuilder
    {
        void OpenDocument();
        void BuildContent(string usernameIn, string descriptionIn, int levelIn);
        void BuildAction(string idIn, string parentIdIn, string usernameIn, int levelIn, string currentUserIn);
        void CloseComment();
        IComment GetComment();
    }
}
