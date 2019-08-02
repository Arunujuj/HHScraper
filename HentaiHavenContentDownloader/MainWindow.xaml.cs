using HentaiHavenContentDownloader.Logic;
using HHScraper.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Series selectedSeries;
        private Episode selectedEpisode;
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
            if(!System.IO.Directory.Exists(HomePathBase + @"\HACD\"))
            {
                System.IO.Directory.CreateDirectory(HomePathBase + @"\HACD\");
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
            comboBox_episodes.Items.Clear();
            episode_thumbnail.Source = null;
            selectedSeries = null;
            int index = comboBox_series.SelectedIndex;
            var seriesList = hentaiLogic.ViewModel.SERIES.Where(x => x.TAGS.Contains(selectedTag)).ToList();
            if (index >= 0)
            {
                selectedSeries = seriesList[index];
                label_name.Content = selectedSeries.NAME;
                textBlock_desc.Text = selectedSeries.DESCRIPTION;
                image_cover.Source = selectedSeries.CoverImage;
                hentaiLogic.GetEpisodes(selectedSeries);
                RefreshEpisodes();
            }
        }

        private void RefreshEpisodes()
        {
            if (selectedSeries != null)
            {
                comboBox_episodes.Items.Clear();
                for (int i = 0; i < selectedSeries.EPISODES.Count(); i++)
                {
                    comboBox_episodes.Items.Add("Video (" + (i+1).ToString() + ")");
                }
            }  
        }

        private void ComboBox_tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTag = comboBox_tags.SelectedItem.ToString();
            RefreshSeries();
        }

        private void ComboBox_episodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedSeries != null)
            {
                int episodeIndex = comboBox_episodes.SelectedIndex;
                if (episodeIndex >= 0)
                {
                    Episode selectedEpisode = selectedSeries.EPISODES[episodeIndex];
                    episode_thumbnail.Source = selectedEpisode.ThumbnailImage;
                }
            }
        }

        private void Image_cover_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(selectedSeries != null)
            {
                Process.Start(selectedSeries.DIRECTURL);
            }
        }
    }
}
