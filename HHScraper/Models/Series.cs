using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHScraper.Models
{
    public class Series
    {
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public List<string> TAGS { get; set; }
        public List<Episode> EPISODES { get; set; }
    }
}
