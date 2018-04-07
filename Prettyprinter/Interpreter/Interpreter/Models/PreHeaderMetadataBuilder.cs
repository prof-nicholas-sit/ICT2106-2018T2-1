using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    class PreHeaderMetadataBuilder : IPreHeaderMetadataBuilder
    {
        private PreHeaderMetadata preHeader;

        public PreHeaderMetadataBuilder()
        {
            // Empty Constructor
        }

        public PreHeaderMetadata GetCompletedHeader()
        {
            return preHeader;
        }

        public void NewHeading(string fontFamily, double fontSize, bool emojiSupport, string lastModified)
        {
            preHeader.FontFamily = fontFamily;
            preHeader.FontSize = fontSize;
            preHeader.EmojiSupport = emojiSupport;
            preHeader.LastModified = lastModified;
        }

        public void OpenPreHeader(int destination)
        {
            preHeader = new PreHeaderMetadata();
            preHeader.Destination = destination;
        }
    }
}
