using HentaiHavenContentDownloader.DataContexts;
using HHScraper;
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
        private HentaiHavenScraper hentaiContent;
        public HentaiSimpleLogic()
        {
            hentaiContent = new HentaiHavenScraper();
            ViewModel = new ViewModel();
        }

        public void GetTags()
        {
            ViewModel.TAGS = new System.Collections.ObjectModel.ObservableCollection<string>(hentaiContent.GetTags());
        }

        public List<T> GetSeries(string tag)
        {

            hentaiContent.

        }

    }
}
