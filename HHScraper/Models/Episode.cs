using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHScraper.Models
{
    public class Episode
    {
        public List<Comment> COMMENTS { get; set; }
        public string THUMBNAIL_URL { get; set; }
        public int VIEWCOUNT { get; set; }
        public int LIKECOUNT { get; set; }

        public List<Episode> SIMILARCONTENT { get; set; }
    }
}
