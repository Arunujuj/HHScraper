using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HHScraper.Models
{
    public class Series
    {
        public string NAME { get; set; }
        public string AIRED { get; set; }
        public string COVER_IMAGE_URL { get; set; }
        public string DESCRIPTION { get; set; }
        public List<string> TAGS { get; set; }
        public List<Episode> EPISODES { get; set; }
        public string DIRECTURL { get; set; }
        public BitmapImage CoverImage
        {
            get
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if(COVER_IMAGE_URL != null && COVER_IMAGE_URL.StartsWith("https://hentaihaven.xxx/"))
                {
                    bitmap.UriSource = new Uri(COVER_IMAGE_URL, UriKind.Absolute);
                }
                else
                {
                    bitmap.UriSource = new Uri("Assets/loliError.jpg", UriKind.Relative);
                }
                bitmap.EndInit();
                return bitmap;
            }
        }
        
    }
}
