using HHScraper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HentaiHavenContentDownloader.DataContexts
{
    public class ViewModel
    {
        private ObservableCollection<string> tags = new ObservableCollection<string>();
        private ObservableCollection<Series> series = new ObservableCollection<Series>();
        private Series selectedSeries = new Series();
        private BitmapImage selectedImage = new BitmapImage();
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

        public ObservableCollection<Series> SERIES
        {
            get
            {
                return series;
            }
            set
            {
                series = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SERIES)));
            }
        }
    }
}
