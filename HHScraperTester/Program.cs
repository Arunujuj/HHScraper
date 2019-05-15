using HHScraper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HHScraperTester
{


    // EXAMPLE LINK: https://hentaihaven.org/koi-maguwai-episode-1/
    // THIS APPLICATION WILL RETURN THE DIRECT VIDEO URL
    // (THE DIRECT VIDEO URL CAN BE DOWNLOADED OR SHARED, NOT LIKE THE VIEWING PAGE)
    
    class Program
    {

        private static Random rnd = new Random();
        static void Main(string[] args)
        {
            HentaiHavenScraper hhs = new HentaiHavenScraper();

            string[] seriesNames = System.IO.File.ReadAllLines("series.txt");
            //System.IO.File.WriteAllLines("series.txt", seriesNames);


            foreach(string series in seriesNames)
            {
                string directorySeriesName = series.Replace(":", string.Empty);
                Thread.Sleep(5000);


                System.IO.Directory.CreateDirectory("hh/" + directorySeriesName);


                Console.WriteLine("===== " + series + " =====");
                System.IO.File.WriteAllText("hh/" + directorySeriesName + "/links.txt", "https://hentaihaven.org/series/" + series);

                string seriesDescription = hhs.GetSeriesDescription(series);
                System.IO.File.WriteAllText("hh/" + directorySeriesName + "/desc.txt", seriesDescription);

                int epiCounter = 1;
                foreach(var episode in hhs.GetEpisodeListFromSeries(series))
                {
                    Thread.Sleep(2000);
                    string episodeThumbnail = hhs.GetThumbnail(episode);
                    System.IO.File.AppendAllText("hh/" + directorySeriesName + "/links.txt", System.Environment.NewLine + "thumbnail: " + episodeThumbnail);
                    string directVideo = hhs.GetHHDirectVideoURL(episode);
                    System.IO.File.AppendAllText("hh/" + directorySeriesName + "/links.txt", System.Environment.NewLine + "video: " + directVideo);
                    Console.WriteLine(epiCounter + ". " + directVideo);
                    epiCounter++;
                }
            }

            Console.ReadLine();
        }
    }
}
