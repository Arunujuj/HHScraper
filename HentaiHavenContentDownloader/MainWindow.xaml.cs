using HentaiHavenContentDownloader.Logic;
using HHScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HentaiHavenContentDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HentaiSimpleLogic hentaiLogic;
        public MainWindow()
        {
            InitializeComponent();
            hentaiLogic = new HentaiSimpleLogic();
            this.DataContext = hentaiLogic.ViewModel;
            hentaiLogic.GetSeries();

            foreach(var series in hentaiLogic.ViewModel.SERIES)
            {
                comboBox_series.Items.Add(series.NAME);
            }

            //hentaiLogic.GetTags();
        }

        private void ComboBox_tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ComboBox_series_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = comboBox_series.SelectedIndex;
            Series selectedSeries = hentaiLogic.ViewModel.SERIES[index];
            label_name.Content = selectedSeries.NAME;
            label_des.Content = selectedSeries.DESCRIPTION;
            image_cover.Source = selectedSeries.CoverImage;
        }
    }
}
