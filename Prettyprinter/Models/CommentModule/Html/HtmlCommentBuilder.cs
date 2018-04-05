using System;
using Prettyprinter.Models.CommentModule;

namespace ThreadTest.Models.CommentModule.Html
{
    class HtmlCommentBuilder : ICommentBuilder
    {
        private HtmlComment doc;
        private string id;
        private string parentId;
        private string username;
        private string description;
        private int level;
        private string currentUser;

        // constructor
        public HtmlCommentBuilder()
        {
            // nothing to do
        }

        // start building a new document
        public void OpenDocument()
        {
            doc = new HtmlComment();

            // erase the title and author
            username = "";
            level = 0;
        }
 
        // build the <content section> element
        public void BuildContent(string usernameIn, string descriptionIn, int levelIn)
        {
            // save the title and author for later
            username = usernameIn;
            description = descriptionIn;
            level = levelIn;

            doc.AppendToBody(new HtmlCommentContent(usernameIn, descriptionIn, levelIn));
        }
        // build the <user action section> element
        public void BuildAction(string idIn, string parentIdIn, string usernameIn, int levelIn, string currentUserIn)
        {
            id = idIn;
            parentId = parentIdIn;
            username = usernameIn;
            level = levelIn;
            currentUser = currentUserIn;

            // add a paragraph containing the text
            doc.AppendToBody(new HtmlCommentAction(id, parentId, username, level, currentUser));
        }

        // finish the document
        public void CloseComment()
        {
            // nothing to do
        }

        // get the document being built
        public IComment GetComment()
        {
            return doc;
        }
    }
}
