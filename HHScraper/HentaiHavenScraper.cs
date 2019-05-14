using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHScraper
{
    public class HentaiHavenScraper
    {

        public string GetHHDirectVideoURL(string baseVideoUrl)
        {
            string resultURL = "";

            var doc = new HtmlDocument();
            doc.LoadHtml(Tools.GetHTML(baseVideoUrl));

            var nodes = doc.DocumentNode;

            foreach (HtmlNode metaTag in doc.DocumentNode.Descendants("video"))
            {
                resultURL = Tools.GetSRC(metaTag.InnerHtml);
            }
            return resultURL;
        }

    }
}
