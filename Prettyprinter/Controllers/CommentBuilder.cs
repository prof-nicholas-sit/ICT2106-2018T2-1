using System;
using Prettyprinter.DAL;
using Prettyprinter.DAL.Gateway;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public class CommentBuilder : DocumentBuilder
    {
        Comment comment;
        ApplicationDbContext db;
        string creationPath;
        CommentManager commentManager = new CommentManager();

        public override void BuildDocument(ApplicationDbContext context, string commentID, string userID, string creationPath,
            string parentID, string Name, bool permission)
        {
            //Initialising components
            db = context;
            this.creationPath = creationPath;

            //Generating new random ID
            string accessControlID = Guid.NewGuid().ToString();

            AccessControl accessControl = new AccessControl(accessControlID, commentID, userID, permission, permission);
            comment = new Comment(commentID, parentID, Name, accessControl);
        }

        public override Document GetDocument()
        {
            return comment;
        }

        public void BuildContent(string content)
        {
            SaveDocument();
            commentManager.writeDocument(creationPath, content, comment._id, true);
        }

        public override void SaveDocument()
        {
            //Storing Comment's Meta Data(Stub Method to store comment into SQL)
            CommentGateway commentGateWay = new CommentGateway(db);
            commentGateWay.CreateFile(comment);

            //Create Comment file on physical server
            commentManager.createDocument(creationPath, comment._id);
        }
    }
}
