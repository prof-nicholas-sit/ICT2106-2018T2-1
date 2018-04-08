using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TypeSetter.Models
{
    public class LevelCheckingModel
    {
        StringBuilder sb;

        String openTag = @"^([A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6}[*]{1})$";
        String closeTag = @"^([*]{1}[A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6})$";
        String OtherTag = @"^([*]{1}[A-z]{5,9}[*]{1})$";

        //List of tags
        List<String> listOfOPENmetadataTag = new List<String> { "/bs*/", "/is*/", "/us*/", "/ss*/", "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/ah1*/", "/ah2*/", "/ul*/", "/ol*/", "/usb*/", "/osl*/", "/pbreak*/", "/hurl*/", "/hlabel*/", "/fc_#FF0000*/", "/taL*/", "/taC*/", "/taR*/", "/taJ*/", "/bq*/", "/th*/" };
        List<String> listOfCLOSEmetadataTag = new List<String> { "/*be/", "/*ie/", "/*ue/", "/*se/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/*ah1/", "/*ah2/", "/*ul/", "/*ol/", "/*usb/", "/*osl/", "/*pbreak/", "/*hurl/", "/*hlabel/", "/*fc_#FF0000/", "/*taL/", "/*taC/", "/*taR/", "/*taJ/", "/*bq/", "/*th/" };
        List<String> listOfOTHERmetadataTag = new List<String> { "/*lbreak*/", "/*ejSmile*/" };

        public LevelCheckingModel()
        {
            sb = new StringBuilder();
        }
        //Check for content's indentation
        public string CheckLevelInStrings(string content)
        {
            String[] line = content.Split("/");

            if (line != null)
            {
                int openCounter = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    String tabString = "";
                    String tempString = "";
                    Match openTagResult = Regex.Match(line[i], openTag);
                    if (openTagResult.Success)
                    {
                        tempString = "/" + line[i] + "/";
                        if (listOfOPENmetadataTag.Contains(tempString))
                        {
                            int index = listOfOPENmetadataTag.IndexOf(tempString);
                            for (int x = 0; x < openCounter; x++)
                            {
                                tabString += "\t";
                            }
                            line[i] = "\n" + tabString + listOfOPENmetadataTag[index] + "\n";
                            openCounter++;
                        }

                    }
                    else
                    {
                        Match closeTagResult = Regex.Match(line[i], closeTag);
                        if (closeTagResult.Success)
                        {
                            tempString = "/" + line[i] + "/";

                            if (listOfCLOSEmetadataTag.Contains(tempString))
                            {
                                openCounter--;
                                for (int x = 0; x < openCounter; x++)
                                {
                                    tabString += "\t";
                                }
                                int index = listOfCLOSEmetadataTag.IndexOf(tempString);
                                line[i] = "\n" + tabString + listOfCLOSEmetadataTag[index];
                            }
                            else
                            {
                                Match OtherTagResult = Regex.Match(line[i], OtherTag);

                                if (OtherTagResult.Success)
                                {
                                    tempString = "/" + line[i] + "/";
                                    if (listOfOTHERmetadataTag.Contains(tempString))
                                    {
                                        int index = listOfOTHERmetadataTag.IndexOf(tempString);
                                        line[i] = tabString + listOfOTHERmetadataTag[index] + "\n";
                                    }
                                }
                                
                            }
                        }
                    }
                    sb.Append(line[i]);
                }
            }
            return sb.ToString();
        }
    }
}
