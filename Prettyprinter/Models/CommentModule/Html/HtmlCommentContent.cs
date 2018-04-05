using System;
using System.Text;

namespace ThreadTest.Models.CommentModule
{
    class HtmlCommentContent : HtmlElement
    {
        private string username;
        private string description;
        private int level;

        // constructor
        public HtmlCommentContent(string usernameIn, string descriptionIn, int levelIn)
        {
            username = usernameIn;
            description = descriptionIn;
            level = levelIn;
        }

        // create a deep clone of the paragraph
        public override HtmlElement Clone()
        {
            return new HtmlCommentContent(username, description, level);
        }

        // paragraphs in HTML are denoted by "<p>...</p>"
        public override string GetString()
        {
            StringBuilder raw = new StringBuilder();
            //================= Col 1 ================
            raw.Append("<div class='col-md-2'>");
            raw.Append("<div class='userProfile level" + level + "'>");


            //if-else, so only level 0s has arrow beside
            if (level > 0)
            {
                raw.Append("<a href=''>" + username + "</a>");
            }
            else
            {
                raw.Append("<span class='glyphicon glyphicon-triangle-right'></span><a href=''>" + username + "</a>");
            }
            raw.Append("</div>");
            raw.Append("</div>");
            //================= Col 1 ================


            //================= Col 2 ================
            //edited input to textarea, the col-md numbers, cancel button, glyphicon icons with hover text,
            raw.Append("<div class='col-md-6'>");
            raw.Append("<div class='commentDetails level" + level + "'>");
            raw.Append("<p>" + description + "</p>");
            raw.Append("</div>");

            //changed position of textarea to after actionbar, and gave levels also
            raw.Append("</div>");

            return raw.ToString();
        }
    }
}
