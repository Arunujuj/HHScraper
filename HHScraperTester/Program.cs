using HHScraper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHScraperTester
{


    // EXAMPLE LINK: https://hentaihaven.org/koi-maguwai-episode-1/
    // THIS APPLICATION WILL RETURN THE DIRECT VIDEO URL
    // (THE DIRECT VIDEO URL CAN BE DOWNLOADED OR SHARED, NOT LIKE THE VIEWING PAGE)


    class Program
    {
        static void Main(string[] args)
        {
            HentaiHavenScraper hhs = new HentaiHavenScraper();
            string inputUrl = Console.ReadLine();
            string url = hhs.GetHHDirectVideoURL(inputUrl);
            if (url != "-")
                Process.Start(url);
            else
                Console.WriteLine("cannot open url");

            Console.ReadLine();
        }
    }
}
