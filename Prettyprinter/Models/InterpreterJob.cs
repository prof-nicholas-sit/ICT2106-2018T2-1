using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Interpreter.Models
{
    public class InterpreterJob
    {
        // Typesetter / Editor will need to pass in data in 1. and 2.

        // 1. Data From Sources (Editor, Typesetter)

        // Destination Flag
        // 0 - Error (No destination found)
        // 1 - To Typesetter (Editor to Typesetter)
        // 2 - To Editor (Typesetter to Editor)
        public int DestinationFlag { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string FileName { get; set; }

        // 2. Document Config for Pre-header
        public string FontFamily { get; set; }
        public double FontSize { get; set; }
        public bool EmojiSupport { get; set; }
        public string LastModified { get; set; }

        // ONLY FOR INTERPRETER: Conversion Data (Formatting in Interpeter)
        public bool IsConverted { get; set; }
        public string ConvertedContent { get; set; }
        public string PreHeaderMetaData { get; set; }
        public string DividerIdentifier { get; set; }

        public InterpreterJob()
        {
            IsConverted = false;
            ConvertedContent = "";
            PreHeaderMetaData = "";
            DividerIdentifier = "";
        }
        
        // Functions
        public string GetFullJobContent()
        {
            if (IsConverted)
            {
                return PreHeaderMetaData + DividerIdentifier + ConvertedContent;
            } else
            {
                return "";
            }
        }
    }
}
