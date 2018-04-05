using System;
namespace ThreadTest.Models.CommentModule
{
    public abstract class HtmlElement
    {
        // create a deep clone of the element
        public abstract HtmlElement Clone();

        // get a string representing the element
        public abstract string GetString();
    }
}
