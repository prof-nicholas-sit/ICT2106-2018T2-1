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

        public override void BuildDocument(ApplicationDbContext context, string commentID, string userID, string parentID, string Name)
        {
            db = context;
            string accessControlID = Guid.NewGuid().ToString();
            AccessControl accessControl = new AccessControl(accessControlID, commentID, userID, true, true);
            comment = new Comment(commentID, parentID, Name, accessControl);
        }

        public override Document GetDocument()
        {
            return comment;
        }

        public void BuildContent(String content)
        {
            SaveDocument();
            //commentManager.populateDocument(id, content);
        }

        public override void SaveDocument()
        {
            CommentGateway commentGateWay = new CommentGateway(db);
            commentGateWay.CreateFile(comment);

            CommentManager commentManager = new CommentManager();
            commentManager.createDocument(comment.ParentId, comment._id);
        }
    }
}
