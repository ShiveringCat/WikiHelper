using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
namespace WikiHelper
{
    public class WikiPage
    {
        public WikiPage(Languages Language, String PageName)
        {
            this.Language = Language;
            HtmlPage = RetrievePage(PageName);
            this.Title = Regex.Match(HtmlPage, "<h. id=" + '"' + "firstHeading" + '"' + " class=" + '"' + "firstHeading" + '"' + " lang=" + '"' + ".*" + '"' + @">([A-Za-z]+)<\/h.>").Groups[1].Value;
            //this.Contents = Regex.Matches(HtmlPage, "<span class=" + '"' + "toctext" + '"' + @">(.*)<\/span>").Cast<Match>().Select(O => O.Groups[1].Value).Reverse().Skip(0).Reverse().ToList();
            this.Contents = Regex.Matches(HtmlPage, "<span class="+'"'+"mw-headline"+'"'+" id="+'"'+@"(.*(?="+'"'+@">))..(.*(?=<\/span))").Cast<Match>().Select(O => O.Groups[2].Value).ToList();
        }
        public List<String> Contents { get; private set; }
        public enum Languages { English }

        public string HtmlPage { get; private set; }

        public Languages Language { get; set; }

        public String Title { get; private set; }

        private string GetPrefixFromLanguage(Languages Language)
        {
            switch (this.Language)
            {
                case Languages.English:
                    return "en";

                default:
                    return "en";
            }
        }

        private string RetrievePage(String PageName)
        {
            var A = new HtmlWeb() { AutoDetectEncoding = false, OverrideEncoding = Encoding.GetEncoding("iso-8859-2") }.Load(@"https://" + GetPrefixFromLanguage(Language) + ".wikipedia.org/wiki/" + PageName);
            return A.DocumentNode.InnerHtml;
        }
    }
}