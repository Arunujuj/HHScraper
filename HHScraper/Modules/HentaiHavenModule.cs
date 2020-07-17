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
        private string baseUrl = "https://hentaihaven.xxx";
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

        public List<Series> GetSeriesList(Tag selectedTag)
        {
            string mainPage = selectedTag.TAG_URL;

            List<Series> allSeries = new List<Series>();
            string url = mainPage + "/page/1";
            var doc = LoadHTMLSite(url);

            var lastButton = doc.DocumentNode.SelectSingleNode("//*[contains(@class,'last')]");

            string lastPageIndexButton = "";

            if(lastButton != null)
            {
                lastPageIndexButton = lastButton.GetAttributeValue("href", "no last page?");

                if (lastPageIndexButton == "no last page?")
                {
                    lastButton = doc.DocumentNode.SelectSingleNode("//*[contains(@class,'nextpostslink')]");
                    lastPageIndexButton = lastButton.GetAttributeValue("href", "no next post?");
                }
            }

            

            int maxPage = 1;
            string[] lastPageSplit;

            if(lastPageIndexButton != "no next post?" && lastPageIndexButton != string.Empty)
            {
                lastPageSplit = lastPageIndexButton.Split('/');
                maxPage = Convert.ToInt32(lastPageSplit[lastPageSplit.Length - 2]);
            }

            for(int pageNumber = 1; pageNumber <= maxPage; pageNumber++)
            {
                string pageUrl = mainPage + "/page/" + pageNumber;
                var pageDoc = LoadHTMLSite(pageUrl);

                var seriesFromPage = pageDoc.DocumentNode.SelectNodes("//div[contains(@class,'page-item-detail')]");

                foreach(var seriesBadge in seriesFromPage)
                {
                    Series nextSeries = new Series();
                    nextSeries.NAME = seriesBadge.ChildNodes[1].ChildNodes[1].GetAttributeValue("title", "no name found");
                    nextSeries.COVER_IMAGE_URL = ""; // remove resolution and the - to get the full hd picture

                    // get full size cover image => by removing the resolution in the url
                    // ex: https://hentaihaven.xxx/www/2020/01/Doctor-Shameless-Nurse-Hentai-uncensored-175x238.jpg

                    string coverURLTiny = seriesBadge.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", "no cover url found");

                    string ext = coverURLTiny.Substring(coverURLTiny.Length - 4, 4);

                    coverURLTiny = coverURLTiny.Substring(0, coverURLTiny.Length - 12);
                    nextSeries.COVER_IMAGE_URL = coverURLTiny + ext;

                    nextSeries.DIRECTURL = seriesBadge.ChildNodes[1].ChildNodes[1].GetAttributeValue("href", "no name directurl");
                    allSeries.Add(nextSeries);
                }

                

            }

            // get page number
            // go over every page
            // get every series
            // collect information if possible










            //string url = baseUrl + "/pick-your-series/";
            //var doc = LoadHTMLSite(url);
            //var Seriesnodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'cat_section')]");
            //foreach (HtmlNode series in Seriesnodes)
            //{
            //    int waitTime = rnd.Next(400, 1000);
            //    Thread.Sleep(waitTime);
            //    // name
            //    HtmlNode seriesMain = series.FirstChild;

            //    HtmlNode seriesInfo = series.LastChild;


            //    Series nextSeries = new Series();
            //    nextSeries.DIRECTURL = seriesMain.GetAttributeValue("href", "no direct :/");
            //    nextSeries.NAME = seriesMain.InnerHtml;
            //    nextSeries.AIRED = GetAiredDate(seriesInfo.InnerHtml);
            //    nextSeries.COVER_IMAGE_URL = seriesInfo.FirstChild.LastChild.FirstChild.GetAttributeValue("src", "no image :(").Replace("-150x150", "");
            //    nextSeries.TAGS = new List<string>();
            //    nextSeries.DESCRIPTION = seriesInfo.LastChild.LastChild.InnerText;
            //    foreach (var tag in seriesInfo.LastChild.LastChild.ChildNodes)
            //    {
            //        if(tag.OriginalName == "a")
            //            nextSeries.TAGS.Add(tag.InnerText.Replace("    ", ""));
            //    }
            //    nextSeries.EPISODES = new List<Episode>();
            //    allSeries.Add(nextSeries);
            //}
            return allSeries;
        }

        public List<Tag> GetTags()
        {
            List<Tag> tags = new List<Tag>();
            var doc = LoadHTMLSite(baseUrl);
            var tagsElements = doc.DocumentNode.SelectNodes("//*[contains(@class,'tagcloud')]")[0];

            foreach(var tagElement in tagsElements.ChildNodes)
            {
                if(tagElement.Name != "#text")
                {
                    Tag nextTag = new Tag();
                    nextTag.TAG_NAME = tagElement.InnerHtml;
                    nextTag.TAG_URL = tagElement.Attributes["href"].Value.ToString();
                    tags.Add(nextTag);
                }
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
                    newEpisode.IndexCount = i + 1;
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
