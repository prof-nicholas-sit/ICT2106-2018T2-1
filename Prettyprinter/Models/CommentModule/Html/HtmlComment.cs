using System;
using System.Collections.Generic;
using System.Text;
using Prettyprinter.Models.CommentModule;

namespace ThreadTest.Models.CommentModule.Html
{
    public class HtmlComment : IComment
    {
        // the elements making up the body of the document
        private IList<HtmlElement> body = new List<HtmlElement>();


        // constructor
        public HtmlComment()
        {
            // nothing to do
        }


        // append an element to the body of the document
        public void AppendToBody(HtmlElement elementIn)
        {
            body.Add(elementIn);
        }




        // get a string representing the document
        public string GetString()
        {
            StringBuilder sbuilder = new StringBuilder();

            // start with the <html> element
            sbuilder.Append("<div class='row'>\n\n");

            // append all of the body elements
            foreach (HtmlElement elem in body)
                sbuilder.Append(elem.GetString());


            // close the <html> element
            sbuilder.Append("</div>");

            return sbuilder.ToString();
        }

    }
}
