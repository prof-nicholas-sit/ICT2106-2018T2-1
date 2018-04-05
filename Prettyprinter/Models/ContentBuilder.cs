using ICT2106Project.Models.Interpreter;
using Interpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    class ContentBuilder : IContentBuilder
    {
        private Content ConvertedContentObject;

        private int Destination;
        private string RawContent;
        private StringBuilder ConvertedContent;

        public ContentBuilder()
        {
            // Empty Constructor
        }

        public void ConvertContent()
        {
            // TODO
            // Loop through content
            // Get the tag and call each formatting model
            // Append to StringBuilder convertedContent
            string[] advanceTagsTT = { "@@`", "==", "==`", ">", ">`" };
            string[] advanceTagsTM = { "/th*/", "/*th/", "/bg*/", "/*bg/", "/*fc/", "/fc_#" };
            string[] alignmentTagsTT = { "</-", "-/>", "<-/>", "<--/>", "<-", "->", "<->", "<-->" };
            string[] alignmentTagsTM = { "/*taL/", "/*taR/", "/*taC/", "/*taJ/", "/taL*/", "/taR*/", "/taC*/", "/taJ*/" };
            string[] basicTT = { "~~", "~~`", "__", "__`", "**", "**`", "--", "--`" };
            string[] basicTM = { "/is*/", "/*ie/", "/us*/", "/*ue/", "/bs*/", "/*be", "/ss*/", "/*se/" };
            string[] emojiTagsTT = { ":smile:", ":wink:", ":joy:", ":fearful:", ":grin:", ":angry:" };
            string[] emojiTagsTM = { "/*ejSmile*/", "/*ejWink*/", "/*ejJoy*/", "/*ejFearful*/", "/*ejGrin*/", "/*ejAngry*/" };
            int emojiMaxLength = 8;
            string[] headerTagsTT = { "#", "##", "###", "####", "#####", "######", "#`", "##`", "###`", "####`", "#####`", "######`" };
            string[] headerTagsTM = { "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/ah1*/ /*ah1/", "/ah2*/ /*ah2/" };
            string[] hyperlinkTagsTT = { "[", "]", "(", ")" };
            string[] hyperlinkTagsTM = { "/hlabel*/", "/*hlabel/", "/hurl*/", "/*hurl/" };
            string[] listTagsTT = { "+", "+`", "++", "++`", "%", "%`", "%%", "%%`" };
            string[] listTagsTM = { "/ul*/", "/*ul/", "/usb*/", "/*usb/", "/ol*/", "/*ol/", "/osl*/", "/*osl/" };
            string[] spacingTagsTT = { "  " };
            string[] spacingTagsTM = { "/*lbreak*/", "/*pbreak*/" };
            string[] allTM = { "/th*/", "/*th/", "/bg*/", "/*bg/", "/*fc/", "/fc_#", //0 - 5
                "/*taL/", "/*taR/", "/*taC/", "/*taJ/", "/taL*/", "/taR*/", "/taC*/", "/taJ*/", //6 - 13
                "/is*/", "/*ie/", "/us*/", "/*ue/", "/bs*/", "/*be", "/ss*/", "/*se/", //14 - 21
                "/*ejSmile*/", "/*ejWink*/", "/*ejJoy*/", "/*ejFearful*/", "/*ejGrin*/", "/*ejAngry*/", //22 - 27
                "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/ah1*/ /*ah1/", "/ah2*/ /*ah2/", //28 - 41
                "/hlabel*/", "/*hlabel/", "/hurl*/", "/*hurl/", //42 - 45
                "/ul*/", "/*ul/", "/usb*/", "/*usb/", "/ol*/", "/*ol/", "/osl*/", "/*osl/", //46 - 53
                "/*lbreak*/", "/*pbreak*/" }; //54 - 55
            int toMarkdownMaxLength = 12;

            string[] temp;
            //Array.Resize(ref temp, temp.Length + 1);
            temp = RawContent.Split('\n');
            string c = "";

            for (int i = 0; i < temp.Length; i++)
            {
                //Start reading char by char
                for (int j = 0; j < temp[i].Length; j++)
                {
                    int index = j;
                    c = temp[i][j].ToString();
                    if (Destination == 1)//Check for TT tags
                    {
                        //If it matches a special character
                        #region ADVANCETAGS
                        //ADVANCE TAGS
                        if ((c.Equals("@") || c.Equals("=") || c.Equals(">")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            if (c.Equals("@"))
                            {
                                //If checking for @, check until the end "`"
                                while (j + 1 < temp[i].Length && (!temp[i][j + 1].Equals(' ')))
                                {
                                    c = c + temp[i][j + 1].ToString();
                                    j++;
                                }
                            }
                            else if (c.Equals("="))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('=') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString();
                                    j++;
                                }
                            }
                            else if (c.Equals(">"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('>') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString();
                                    j++;
                                }
                            }
                            //Find which special tag it belongs to
                            foreach (string tags in advanceTagsTT)
                            {
                                //Special handling for @@
                                if (c.Contains("@@"))
                                {
                                    Advance advance = new Advance();
                                    String advanceConv = advance.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, advanceConv);
                                    break;
                                }
                                if (c.Equals(tags))
                                {
                                    Advance advance = new Advance();
                                    String advanceConv = advance.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, advanceConv);
                                }
                            }
                        }
                        #endregion ADVANCETAGS
                        #region BASICTAGS
                        //BASIC TAGS
                        if ((c.Equals("~") || c.Equals("_") || c.Equals("*") || c.Equals("-")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            if (c.Equals("~"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('~') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("_"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('_') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("*"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('*') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("-"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('-') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            //Find which special tag it belongs to
                            foreach (string tags in basicTT)
                            {
                                if (c.Equals(tags))
                                {
                                    Basic basic = new Basic();
                                    String basicConv = basic.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, basicConv);
                                }
                            }
                        }
                        #endregion BASICTAGS
                        #region ALIGNMENTTAGS
                        //ALIGNMENT TAGS
                        if ((c.Equals("<") || c.Equals("/") || c.Equals("-")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            if (c.Equals("<"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('/') || temp[i][j + 1].Equals('-') || temp[i][j + 1].Equals('>')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("-"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('/') || temp[i][j + 1].Equals('>')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            //Find which special tag it belongs to
                            foreach (string tags in alignmentTagsTT)
                            {
                                if (c.Equals(tags))
                                {
                                    Alignment alignment = new Alignment();
                                    String alignmentConv = alignment.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, alignmentConv);
                                }
                            }
                        }
                        #endregion ALIGNMENTTAGS
                        #region EMOJITAGS
                        //EMOJI TAGS
                        if (c.Equals(":") && j + 1 < temp[i].Length)
                        {
                            //Length counter
                            int length = 0;
                            //Finish reading the rest of the tag
                            while (!temp[i][j + 1].Equals(':') && length != emojiMaxLength)
                            {
                                c = c + temp[i][j + 1].ToString(); ;
                                length++;
                                j++;
                            }
                            if (length == emojiMaxLength) { continue; }//no more ':' found, probably not an emoji...
                            c += ":";
                            //Find which special tag it belongs to
                            foreach (string tags in emojiTagsTT)
                            {
                                if (c.Equals(tags))
                                {
                                    Emoji emoji = new Emoji();
                                    String emojiConv = emoji.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, emojiConv);
                                }
                            }
                        }
                        #endregion EMOJITAGS
                        #region HEADERTAGS
                        //HEADERTAGS TAGS
                        if ((c.Equals("#") || c.Equals("!") || c.Equals("&")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            if (c.Equals("#"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('#') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("!"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('!')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("&"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('&')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            //Find which special tag it belongs to
                            if (c.Equals("!!!") || c.Equals("&&&"))
                            {
                                Header header = new Header();
                                String headerConv = header.ToTypesetter(c);
                                string[] headerSplit = headerConv.Split(' ');
                                temp[i - 1] = temp[i - 1].Insert(0, headerSplit[0]);
                                temp[i - 1] = temp[i - 1].Insert(temp[i - 1].Length, headerSplit[0]);
                                temp[i] = temp[i].Replace("!!!", "").Replace("&&&", "");
                                //To remove if the line does not contain any more characters?...
                            }
                            foreach (string tags in headerTagsTT)
                            {
                                //Special handling for !!! and &&&
                                if (c.Equals(tags))
                                {
                                    Header header = new Header();
                                    String headerConv = header.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, headerConv);
                                }
                            }
                        }
                        #endregion HEADERTAGS
                        #region HYPERLINKTAGS
                        //HYPERLINK TAGS
                        if (c.Equals("[") || c.Equals("]") || c.Equals("(") || c.Equals(")"))
                        {
                            foreach (string tags in hyperlinkTagsTT)
                            {
                                if (c.Equals(tags))
                                {
                                    Hyperlink hyperlink = new Hyperlink();
                                    String hyperlinkConv = hyperlink.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, hyperlinkConv);
                                }
                            }
                        }
                        #endregion HYPERLINKTAGS
                        #region LISTTAGS
                        //LIST TAGS
                        if ((c.Equals("+") || c.Equals("%")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            if (c.Equals("+"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('+') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            else if (c.Equals("%"))
                            {
                                while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals('%') || temp[i][j + 1].Equals('`')))
                                {
                                    c = c + temp[i][j + 1].ToString(); ;
                                    j++;
                                }
                            }
                            //Find which special tag it belongs to
                            foreach (string tags in listTagsTT)
                            {
                                if (c.Equals(tags))
                                {
                                    List list = new List();
                                    String listConv = list.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, listConv);
                                }
                            }
                        }
                        #endregion LISTTAGS
                        #region SPACINGTAGS
                        //SPACING TAGS
                        if ((c.Equals(" ")) && j + 1 < temp[i].Length)
                        {
                            //Finish reading the rest of the tag
                            while (j + 1 < temp[i].Length && (temp[i][j + 1].Equals(' ')))
                            {
                                c = c + temp[i][j + 1].ToString(); ;
                                j++;
                            }
                            //Find which special tag it belongs to
                            foreach (string tags in spacingTagsTT)
                            {
                                if (c.Contains(tags))
                                {
                                    Spacing spacing = new Spacing();
                                    String spacingConv = spacing.ToTypesetter(c);
                                    temp[i] = temp[i].Remove(index, c.Length);
                                    temp[i] = temp[i].Insert(index, spacingConv);
                                }
                            }
                        }
                        #endregion SPACINGTAGS
                    }
                    else if (Destination == 2)
                    {
                        if (c.Equals("/") && j + 1 < temp[i].Length)
                        {
                            //Length counter
                            int length = 0;
                            //Finish reading the rest of the tag
                            while ((j + 1 < temp[i].Length && !temp[i][j + 1].Equals('/')) || (j + 1 < temp[i].Length && c.Contains("ah") && (!c.Contains("/*ah1") || !c.Contains("/*ah2"))))
                            {
                                c = c + temp[i][j + 1].ToString();
                                length++;
                                j++;
                            }
                            c += "/";
                            //Special handling for Alternative H1 & H2
                            if (c.Contains("ah"))
                            {
                                if (c.Contains("ah1"))
                                {
                                    c = c.Remove(c.Length-1, 1);
                                    string[] altHeaderSplit = c.Split(new string[] { "/ah1*/", "/*ah1/" }, StringSplitOptions.None);
                                    altHeaderSplit[0] = "/ah1*/";
                                    altHeaderSplit[2] = "/*ah1/";
                                    temp[i] = temp[i].Remove(index, altHeaderSplit[0].Length);
                                    temp[i] = temp[i].Remove(temp[i].Length - altHeaderSplit[2].Length, altHeaderSplit[2].Length);
                                    c = c.Replace(altHeaderSplit[1], " ");
                                }
                            }

                            //Find which special tag it belongs to
                            for (int t = 0; t < allTM.Length; t++)
                            {
                                if (c.Contains(allTM[t]))
                                {
                                    String conv;
                                    if (t < 6)
                                    {
                                        Advance tag = new Advance();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else if (t < 14)
                                    {
                                        Alignment tag = new Alignment();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else if (t < 22)
                                    {
                                        Basic tag = new Basic();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else if (t < 28)
                                    {
                                        Emoji tag = new Emoji();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else if (t < 42)
                                    {
                                        Header tag = new Header();
                                        conv = tag.ToMarkdown(c);
                                        if (!c.Contains("ah"))
                                        {
                                            temp[i] = temp[i].Remove(index, c.Length);
                                            temp[i] = temp[i].Insert(index, conv);
                                        }
                                        //Special handling for Alternative H1 & H2
                                        else
                                        {
                                            temp[i] = temp[i + 1].Insert(0, conv);
                                        }
                                    }
                                    else if (t < 46)
                                    {
                                        Hyperlink tag = new Hyperlink();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else if (t < 54)
                                    {
                                        List tag = new List();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    else
                                    {
                                        Spacing tag = new Spacing();
                                        conv = tag.ToMarkdown(c);
                                        temp[i] = temp[i].Remove(index, c.Length);
                                        temp[i] = temp[i].Insert(index, conv);
                                    }
                                    j = conv.Length-1;
                                }
                            }
                        }
                    }
                }
            }
            ConvertedContent = new StringBuilder();
            for(int i=0; i < temp.Length; i++)
            {
                ConvertedContent.Append(temp[i]);
                ConvertedContent.Append('\n');
            }
        }

        public Content GetConvertedContent()
        {
            return ConvertedContentObject;
        }

        public void OpenContentBuilder(int destination, string content)
        {
            ConvertedContentObject = new Content();

            Destination = destination;
            RawContent = content;

            ConvertedContentObject.Destination = Destination;
            ConvertedContentObject.RawContent = RawContent;
        }
    }
}
