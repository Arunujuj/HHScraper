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
        private HtmlDocument LoadHTMLSite(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Tools.GetHTML(url));
            return doc;
        }



        public string GetHHDirectVideoURL(string baseVideoUrl)
        {
            string resultURL = "";

            var doc = LoadHTMLSite(baseVideoUrl);

            var nodes = doc.DocumentNode;

            foreach (HtmlNode metaTag in doc.DocumentNode.Descendants("video"))
            {
                resultURL = Tools.GetSRC(metaTag.InnerHtml);
            }
            return resultURL;
        }



        public string[] GetSeriesList()
        {
            string baseUrl = "https://hentaihaven.org/pick-your-series/";
            var doc = LoadHTMLSite(baseUrl);
            var nodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'category_alphabet_title_link')]");
            string[] names = new string[nodes.Count];

            for(int i = 0; i<nodes.Count; i++)
            {
                string seriesName = nodes[i].InnerHtml;
                seriesName = seriesName.Replace("...", string.Empty);
                seriesName = seriesName.Replace("!", string.Empty);
                names[i] = seriesName.Replace(" ", "-");
            }
            return names;
        }

        public string GetSeriesDescription(string seriesName)
        {
            string baseUrl = "https://hentaihaven.org/series/" + seriesName;
            string description = "";

            var doc = LoadHTMLSite(baseUrl);

            var nodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'archive-meta category-meta')]");
            description = nodes.FirstOrDefault().InnerText;
            return description;

        }

        public string GetThumbnail(string baseVideoUrl)
        {
            string thumbnailURL = "";

            var doc = LoadHTMLSite(baseVideoUrl);

            foreach (HtmlNode metaTag in doc.DocumentNode.Descendants("video"))
            {
                thumbnailURL = metaTag.GetAttributeValue("poster", "-");
            }
            return thumbnailURL;
        }

        public string[] GetEpisodeListFromSeries(string seriesName)
        {
            string baseUrl = "https://hentaihaven.org/series/" + seriesName; 
            var doc = LoadHTMLSite(baseUrl);
            var nodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'brick-title')]");
            string[] episodes = new string[nodes.Count];
            for(int i = 0; i < nodes.Count; i++)
            {
                episodes[i] = nodes[i].GetAttributeValue("href", "-");
            }
            return episodes;
        }


    }
}
