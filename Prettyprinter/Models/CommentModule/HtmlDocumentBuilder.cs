using System;
namespace ThreadTest.Models.CommentModule
{
    class HtmlDocumentBuilder 
    {
        private HtmlDocument doc;
        private string id;
        private string parentId;
        private string username;
        private string description;
        private int level;
        private string currentUser;

        // constructor
        public HtmlDocumentBuilder()
        {
            // nothing to do
        }

        // start building a new document
        public void OpenDocument()
        {
            doc = new HtmlDocument();

            // erase the title and author
            username = "";
            level = 0;
        }
        // build the header
        public void BuildHead(string titleIn, string authorIn)
        {
        }

        // build the body
        public void BuildBody(string textIn)
        {
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
        public void CloseDocument()
        {
            // nothing to do
        }

        // get the document being built
        public HtmlDocument GetDocument()
        {
            return doc;
        }
    }
}
