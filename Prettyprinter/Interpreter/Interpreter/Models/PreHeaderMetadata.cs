using Interpreter.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    class PreHeaderMetadata
    {
        public int Destination { get; set; }
        public string FontFamily { get; set; }
        public double FontSize { get; set; }
        public bool EmojiSupport { get; set; }
        public string LastModified { get; set; }

        public string GetPreHeader()
        {
            if (Destination == 1)
            {
                TypesetterPreHeadings ts = new TypesetterPreHeadings();
                return ts.GetStart() +
                    ts.GetFont(FontFamily) +
                    ts.GetFontSize(FontSize) +
                    ts.GetEmojiSupport(EmojiSupport) +
                    ts.GetLastModified(LastModified) +
                    ts.GetEnd();
            }

            else if (Destination == 2)
            {
                MarkdownPreHeadings md = new MarkdownPreHeadings();
                return md.GetStart() +
                    md.GetFont(FontFamily) +
                    md.GetFontSize(FontSize) +
                    md.GetEmojiSupport(EmojiSupport) +
                    md.GetLastModified(LastModified) +
                    md.GetEnd();
            } 

            else
            {
                return "";
            }
        }
    }
}
