using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TypeSetter.Models
{
    public class ReverseConvertModel
    {
        //Local variables
        StringBuilder sb;

		//For zul to write the regx
		String openTag = @"^([A-z]{1,10}[1-6]{0,1}[>]{1})$";
		String closeTag = @"^([/]{1}[A-z]{1,10}[1-6]{0,1}[>]{1})$";

		//List of tags
		List<String> listOfOPENmetadataTag = new List<String> { "/bs*/", "/is*/", "/us*/", "/ss*/", "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/ah1*/", "/ah2*/", "/ul*/", "/ol*/", "/usb*/", "/osl*/", "/pbreak*/", "/hurl*/", "/hlabel*/", "/fc_#FF0000*/", "/taL*/", "/taC*/", "/taR*/", "/taJ*/", "/bq*/", "/th*/" , "/*lbreak*/"};
        List<String> listOfCLOSEmetadataTag = new List<String> { "/*be/", "/*ie/", "/*ue/", "/*se/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/*ah1/", "/*ah2/", "/*ul/", "/*ol/", "/*usb/", "/*osl/", "/*pbreak/", "/*hurl/", "/*hlabel/", "/*fc_#FF0000/", "/*taL/", "/*taC/", "/*taR/", "/*taJ/", "/*bq/", "/*th/" , ""};

        List<String> listOfOPENhtmlTag = new List<String> { "<b>", "<i>", "<u>", "<strike>", "<h1>", "<h2>", "<h3>", "<h4>", "<h5>", "<h6>", "<h1><alt='", "<h2><alt='", "<ul>", "<ol>", "<ul><li>", "<ol><li>", "<p>", "<a href='", "", "<font color='", "<div align='left'>", "<center>", "<div align='right'>", "<div align='justify'>", "<blockquote cite='", "<mark>","<br>" };
        List<String> listOfCLOSEhtmlTag = new List<String> { "</b>", "</i>", "</u>", "</strike>", "</h1>", "</h2>", "</h3>", "</h4>", "</h5>", "</h6>", "'></h1>", "'></h2>", "</ul>", "</ol>", "</li></ul>", "</li></ol>", "</p>", "'>", "</a>", "'></font>", "</div>", "</center>", "</div>", "</div>", "'></blockquote>", "</mark>","" };

        //constructor
        public ReverseConvertModel(){
            sb = new StringBuilder();
        }

        //Reverse convert for MarkUpInterpreter
        public string convertBackToMarkUp(String html)
        {
            String[] line = html.Split("\n");

            if(line != null){
                for (int i = 0; i < line.Length; i++){
                    String[] lineSplit = line[i].Split("<");
                    String tempString = "" ;

                    for (int j = 0; j < lineSplit.Length; j++){

                        Match openTagResult = Regex.Match(lineSplit[j], openTag);

                        if (openTagResult.Success)
                        {
                            //replace the text
                            tempString = "<" + lineSplit[j];

                            if (listOfOPENhtmlTag.Contains(tempString))
                            {
                                int index = listOfOPENhtmlTag.IndexOf(tempString);
                                lineSplit[j] = listOfOPENmetadataTag[index];
                            }

                        }
                        else {
                            Match closeTagResult = Regex.Match(lineSplit[j], closeTag);

                            if (closeTagResult.Success)
                            {
                                tempString = "<" + lineSplit[j];

                                if (listOfCLOSEhtmlTag.Contains(tempString))
                                {
                                    int index = listOfCLOSEhtmlTag.IndexOf(tempString);
                                    lineSplit[j] = listOfCLOSEmetadataTag[index];
                                }
                            }
                            else {
                                continue;
                            }

                        }
                        sb.Append(lineSplit[j]);
                    }
                    sb.Append("\n");
                }

            }

            return sb.ToString();
        }
    }
}
