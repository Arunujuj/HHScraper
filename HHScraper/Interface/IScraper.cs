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
        List<string> GetTags();
        List<Series> GetSeriesList();
        string GetVideoThumbnailURL(string videoUrl);
    }
}
