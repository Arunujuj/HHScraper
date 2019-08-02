using HHScraper.Interface;
using HHScraper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HHScraper.Modules
{
    public class HentaiHavenModule : IScraper
    {
        #region HentaiHaven specific privates
        private string baseUrl = "https://hentaihaven.org";
        private Random rnd = new Random();



        private HtmlDocument LoadHTMLSite(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Tools.GetHTML(url));
            return doc;
        }

        private string GetAiredDate(string html)
        {
            if(html.Contains("Aired:"))
            {
                html = html.Remove(0, html.IndexOf("Aired:")).Replace("</strong>", string.Empty);
                html = html.Replace("Aired: ", string.Empty);
                html = html.Remove(0, 1);
                html = html.Remove(4, html.Length - 4);
            }
            else
            {
                html = "unknown";
            }
            

            string result = html;
            return result;
        }

        #endregion

        public List<Series> GetSeriesList()
        {
            List<Series> allSeries = new List<Series>();
            string url = baseUrl + "/pick-your-series/";
            var doc = LoadHTMLSite(url);
            var Seriesnodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'cat_section')]");
            foreach (HtmlNode series in Seriesnodes)
            {
                int waitTime = rnd.Next(400, 1000);
                Thread.Sleep(waitTime);
                // name
                HtmlNode seriesMain = series.FirstChild;

                HtmlNode seriesInfo = series.LastChild;


                Series nextSeries = new Series();
                nextSeries.DIRECTURL = seriesMain.GetAttributeValue("href", "no direct :/");
                nextSeries.NAME = seriesMain.InnerHtml;
                nextSeries.AIRED = GetAiredDate(seriesInfo.InnerHtml);
                nextSeries.COVER_IMAGE_URL = seriesInfo.FirstChild.LastChild.FirstChild.GetAttributeValue("src", "no image :(").Replace("-150x150", "");
                nextSeries.TAGS = new List<string>();
                nextSeries.DESCRIPTION = seriesInfo.LastChild.LastChild.InnerText;
                foreach (var tag in seriesInfo.LastChild.LastChild.ChildNodes)
                {
                    if(tag.OriginalName == "a")
                        nextSeries.TAGS.Add(tag.InnerText.Replace("    ", ""));
                }
                nextSeries.EPISODES = new List<Episode>();
                allSeries.Add(nextSeries);
            }
            return allSeries;
        }

        public List<string> GetTags()
        {
            List<string> tags = new List<string>();
            string baseUrl = "https://hentaihaven.org/pick-your-poison/";
            var doc = LoadHTMLSite(baseUrl);
            var tagsElements = doc.DocumentNode.SelectNodes("//*[contains(@class,'tooltip-wrapper')]");
            foreach (HtmlNode tag in tagsElements)
            {
                tags.Add(tag.InnerText);
            }
            return tags;
        }

        public string GetVideoThumbnailURL(string videoUrl)
        {
            string thumbnailURL = "";
            var doc = LoadHTMLSite(videoUrl);
            foreach (HtmlNode metaTag in doc.DocumentNode.Descendants("video"))
            {
                thumbnailURL = metaTag.GetAttributeValue("poster", "noPoster");
            }
            return thumbnailURL;
        }

        public string GetDirectVideoDownload(string videoUrl)
        {
            string thumbnailURL = "";
            var doc = LoadHTMLSite(videoUrl);
            foreach (HtmlNode metaTag in doc.DocumentNode.Descendants("video"))
            {
                thumbnailURL = metaTag.ChildNodes[1].GetAttributeValue("src", "noDownload");
            }
            return thumbnailURL;
        }

        public List<Episode> GetEpisodes(Series series)
        {
            List<Episode> episodes = new List<Episode>();
            string episodeListUrl = series.DIRECTURL;
            var doc = LoadHTMLSite(episodeListUrl);
            var list = doc.DocumentNode.SelectNodes("//*[contains(@class,'thumbnail-image')]");
            if(list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Episode newEpisode = new Episode();
                    newEpisode.DIRECT_URL = list[i].GetAttributeValue("href", "noDirectUrl");
                    newEpisode.THUMBNAIL_URL = list[i].ChildNodes[1].GetAttributeValue("data-src", "noThumbnail");
                    newEpisode.DirectDownloadMp4 = GetDirectVideoDownload(newEpisode.DIRECT_URL);

                    episodes.Add(newEpisode);
                }
            }
            
            return episodes;
        }
    }
}
