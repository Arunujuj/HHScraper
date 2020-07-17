using HHScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHScraper.Interface
{
    public interface IScraper
    {
        List<Tag> GetTags();
        List<Series> GetSeriesList(Tag selectedTag);
        List<Episode> GetEpisodes(Series series);
        string GetVideoThumbnailURL(string videoUrl);
        string GetDirectVideoDownload(string videoUrl);
    }
}
