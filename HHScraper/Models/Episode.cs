using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HHScraper.Models
{
    public class Episode
    {
        public List<Comment> COMMENTS { get; set; }
        public string THUMBNAIL_URL { get; set; }
        public int VIEWCOUNT { get; set; }
        public int LIKECOUNT { get; set; }
        public string DIRECT_URL { get; set; }
        public List<Episode> SIMILARCONTENT { get; set; }
        public string DirectDownloadMp4 { get; set; }
        public BitmapImage ThumbnailImage
        {
            get
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if (THUMBNAIL_URL != null && THUMBNAIL_URL.StartsWith("https://hentaihaven.org/"))
                {
                    bitmap.UriSource = new Uri(THUMBNAIL_URL, UriKind.Absolute);
                }
                else
                {
                    bitmap.UriSource = new Uri("Assets/loliError.jpg", UriKind.Relative);
                }
                bitmap.EndInit();
                return bitmap;
            }
        }

        public int IndexCount { get; set; }
    }
}
