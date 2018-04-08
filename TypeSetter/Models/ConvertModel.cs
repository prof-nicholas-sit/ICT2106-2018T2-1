using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TypeSetter.Models
{
    public class ConvertModel
    {
		//Local variables
		StringBuilder sb;

		String openTag = @"^([A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6}[*]{1})$";
		String closeTag = @"^([*]{1}[A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6})$";
		String OtherTag = @"^([*]{1}[A-z]{5,9}[*]{1})$";

		//List of tags
		List<String> listOfOPENmetadataTag = new List<String> { "/bs*/", "/is*/", "/us*/", "/ss*/", "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/ah1*/", "/ah2*/", "/ul*/", "/ol*/", "/usb*/", "/osl*/", "/hurl*/", "/hlabel*/", "/fc_#FF0000*/", "/taL*/", "/taC*/", "/taR*/", "/taJ*/", "/bq*/", "/th*/" };
		List<String> listOfCLOSEmetadataTag = new List<String> { "/*be/", "/*ie/", "/*ue/", "/*se/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/*ah1/", "/*ah2/", "/*ul/", "/*ol/", "/*usb/", "/*osl/", "/*hurl/", "/*hlabel/", "/*fc_#FF0000/", "/*taL/", "/*taC/", "/*taR/", "/*taJ/", "/*bq/", "/*th/" };
		List<String> listOfOTHERmetadataTag = new List<String> { "/*pbreak*/", "/*lbreak*/", "/*ejSmile*/" };

		List<String> listOfOPENhtmlTag = new List<String> { "<b>", "<i>", "<u>", "<strike>", "<h1>", "<h2>", "<h3>", "<h4>", "<h5>", "<h6>", "<h1><alt='", "<h2><alt='", "<ul>", "<ol>", "<ul><li>", "<ol><li>", "<a href='", "", "<font color='", "<div align='left'>", "<center>", "<div align='right'>", "<div align='justify'>", "<blockquote cite='", "<mark>" };
		List<String> listOfCLOSEhtmlTag = new List<String> { "</b>", "</i>", "</u>", "</strike>", "</h1>", "</h2>", "</h3>", "</h4>", "</h5>", "</h6>", "'></h1>", "'></h2>", "</ul>", "</ol>", "</li></ul>", "</li></ol>", "'>", "</a>", "'></font>", "</div>", "</center>", "</div>", "</div>", "'></blockquote>", "</mark>" };
		List<String> listOfOTHERhtmlTag = new List<String> { "<br><br>", "<br>", "&#1F600" };


		//constructor
		public ConvertModel() {
			sb = new StringBuilder();
		}

        //Convert Content from FileManager for Edit function from them
        public string convertToHTML(String content)
        {
			String[] line = content.Split("\n");

			if (line != null) {
				for (int i = 0; i < line.Length; i++) {
					String[] lineSplit = line[i].Split("/");
					String tempString = "";

					for (int j = 0; j < lineSplit.Length; j++) {

						Match openTagResult = Regex.Match(lineSplit[j], openTag);

						if (openTagResult.Success) {
							//replace the text
							tempString = "/" + lineSplit[j] + "/";

							if (listOfOPENmetadataTag.Contains(tempString)) {
								int index = listOfOPENmetadataTag.IndexOf(tempString);
								lineSplit[j] = listOfOPENhtmlTag[index];
							}

						}
						else {
							Match closeTagResult = Regex.Match(lineSplit[j], closeTag);

							if (closeTagResult.Success)
							{
								tempString = "/" + lineSplit[j] + "/";

								if (listOfCLOSEmetadataTag.Contains(tempString))
								{
									int index = listOfCLOSEmetadataTag.IndexOf(tempString);
									lineSplit[j] = listOfCLOSEhtmlTag[index];
								}
							}
							else {
								//OTHER
								Match OtherTagResult = Regex.Match(lineSplit[j], OtherTag);

								if (OtherTagResult.Success) {
									tempString = "/" + lineSplit[j] + "/";

									if (listOfOTHERmetadataTag.Contains(tempString)) {
										int index = listOfOTHERmetadataTag.IndexOf(tempString);
										lineSplit[j] = listOfOTHERhtmlTag[index];
									}
								}
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
