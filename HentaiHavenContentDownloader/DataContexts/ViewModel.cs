using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiHavenContentDownloader.DataContexts
{
    public class ViewModel
    {
        private ObservableCollection<string> tags = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> TAGS
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TAGS)));
            }
        }


    }
}
