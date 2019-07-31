using HentaiHavenContentDownloader.Logic;
using HHScraper.Models;
using MahApps.Metro.Controls;
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
    public partial class MainWindow
    {
        public HentaiSimpleLogic hentaiLogic;
        private string selectedTag = "";
        private static string HomePathBase = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public MainWindow()
        {
            InitializeComponent();
            hentaiLogic = new HentaiSimpleLogic();
            this.DataContext = hentaiLogic.ViewModel;

            InitCacheFolder();
            RefreshTags();
        }

        private void InitCacheFolder()
        {
            if(!System.IO.Directory.Exists(HomePathBase + @"\HHCD\"))
            {
                System.IO.Directory.CreateDirectory(HomePathBase + @"\HHCD\");
                RefreshTags();
                hentaiLogic.GetSeries();
                hentaiLogic.SaveCache(HomePathBase);
            }
            else
            {
                hentaiLogic.LoadCache(HomePathBase);
            }
        }

        private void RefreshSeries()
        {
            comboBox_series.Items.Clear();
            var seriesList = hentaiLogic.ViewModel.SERIES.Where(x => x.TAGS.Contains(selectedTag));
            foreach (var series in seriesList)
            {
                comboBox_series.Items.Add(series.NAME);
            }
        }

        private void RefreshTags()
        {
            comboBox_tags.Items.Clear();
            hentaiLogic.GetTags();
            foreach(var tag in hentaiLogic.ViewModel.TAGS)
            {
                comboBox_tags.Items.Add(tag.ToString());
            }
        }

        private void ComboBox_series_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = comboBox_series.SelectedIndex;
            var seriesList = hentaiLogic.ViewModel.SERIES.Where(x => x.TAGS.Contains(selectedTag)).ToList();
            if (index >= 0)
            {
                Series selectedSeries = seriesList[index];
                label_name.Content = selectedSeries.NAME;
                textBlock_desc.Text = selectedSeries.DESCRIPTION;
                image_cover.Source = selectedSeries.CoverImage;
            }
            
        }

        private void ComboBox_tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTag = comboBox_tags.SelectedItem.ToString();
            RefreshSeries();
        }
    }
}
