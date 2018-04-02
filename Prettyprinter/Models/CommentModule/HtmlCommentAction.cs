using System;
using System.Text;
using ThreadTest.Controllers;

namespace ThreadTest.Models.CommentModule
{
    class HtmlCommentAction : HtmlElement
    {
        private string id;
        private string parentId;
        private string username;
        private int level;
        private string currentUser;

        CommentController cc = new CommentController();

        // constructor
        public HtmlCommentAction(string idIn, string parentIdIn, string usernameIn, int levelIn, string currentUserIn)
        {
            id = idIn;
            parentId = parentIdIn;
            username = usernameIn;
            level = levelIn;
            currentUser = currentUserIn;
        }

        // create a deep clone of the paragraph
        public override HtmlElement Clone()
        {
            return new HtmlCommentAction(id, parentId, username, level, currentUser);
        }

        // paragraphs in HTML are denoted by "<p>...</p>"
        public override string GetString()
        {
            StringBuilder raw = new StringBuilder();

            raw.Append("<div class='col-md-3 actionBar'>");
            raw.Append("<div class='userActions'>");
            if (username.Equals(currentUser))
            {
                raw.Append("<a href='#' onclick='showReplyBox(\"edit" + id + "\")'><span class='glyphicon glyphicon-pencil' title='Edit'></span></a> | <a href='/Comment/Delete?id=" + id + "'><span class='glyphicon glyphicon-trash' title='Delete'></span></a> | ");
            }
            raw.Append("<a href='#' onclick='showReplyBox(\"reply" + id + "\")'><span class='glyphicon glyphicon-share-alt' title='Reply'></span></a> | <a href='/Comment/Like?id=" + id + "'><span class='glyphicon glyphicon-thumbs-up' title='Like'></span></a> | <a href='/Comment/Permalink'><span class='glyphicon glyphicon-pushpin' title='Permalink'></span></a></div>");
            raw.Append(cc.LikeThis(Int32.Parse(id)) + " likes");

            raw.Append("</div>");


            if (username.Equals(currentUser))
            {
                raw.Append("<form id='edit" + id + "' class='hiddenForm' action='/Comment/Edit' method='post'>");
                raw.Append("<div class='form-group level" + level + "'>");
                raw.Append("<textarea rows='3' cols='10' class='form-control' type='text' id='Description' name='Description' value=''></textarea><br/>");
                raw.Append("<input type='hidden' id='ParentId' name='ParentId' value=" + parentId + ">");
                raw.Append("<input type='hidden' id='Id' name='Id' value=" + id + ">");
                //raw.Append("<input type='submit' value='Save Edit' class='btn btn-default'> | <a href='#' onclick='hideReplyBox(\"edit" + item.Id + "\")'>Cancel</a> ");
                raw.Append("<input type='submit' value='Save Edit' class='btn-sm btn-default'> <button type='button' onclick='hideReplyBox(\"edit" + id + "\")' class='btn-sm btn-basic'>Cancel</button> ");

                raw.Append("</div>");
                raw.Append("</form>");
            }

            raw.Append("<form id='reply" + id + "' class='hiddenForm' action='/Comment/Create' method='post'>");

            raw.Append("<div class='form-group level" + level + "'>");
            raw.Append("<textarea rows='3' cols='10' class='form-control' type='text' id='Description' name='Description' value=''></textarea><br/>");
            raw.Append("<input type='hidden' id='id' name='id' value=" + id + ">");
            raw.Append("<input type='submit' value='Reply' class='btn-sm btn-default'> <button type='button' onclick='hideReplyBox(\"reply" + id + "\")' class='btn-sm btn-basic'>Cancel</button> ");
            raw.Append("</div>");
            raw.Append("</form>");

            return raw.ToString();
        }
    }
}
