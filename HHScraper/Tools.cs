using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HHScraper
{
    public static class Tools
    {
        public static string GetHTML(string url)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }
        }
        public static string GetSRC(string innerHtml)
        {
            if (innerHtml != "")
            {
                string result = "";

                int startIndex = innerHtml.IndexOf("src=") + 5;
                result = innerHtml.Remove(0, startIndex);
                int endIndex = result.IndexOf("\" ");
                result = result.Remove(endIndex, result.Length - endIndex);


                return result;
            }
            return "";
        }

        public static void SaveImage(string imageUrl, ImageFormat format, string savePath)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(savePath, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
    }
}
