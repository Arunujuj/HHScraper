using HentaiHavenContentDownloader.DataContexts;
using HHScraper;
using HHScraper.Interface;
using HHScraper.Models;
using HHScraper.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiHavenContentDownloader.Logic
{
    // passes calls onto the core library HHScraper to perform certain tasks,
    // evaluates the information and does something with it.
    // thanks to the ViewModel, theres no need to return any information
    // every task in here is managed "in house"
    public class HentaiSimpleLogic
    {
        public ViewModel ViewModel;
        private IScraper scrapingContent;
        public HentaiSimpleLogic()
        {
            scrapingContent = new HentaiHavenModule();
            ViewModel = new ViewModel();
        }

        public void GetTags()
        {
            ViewModel.TAGS = new System.Collections.ObjectModel.ObservableCollection<string>(scrapingContent.GetTags());
        }

        public void GetSeries()
        {
            ViewModel.SERIES = new System.Collections.ObjectModel.ObservableCollection<Series>(scrapingContent.GetSeriesList());
        }

    }
}
