using HentaiHavenContentDownloader.Logic;
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
            hentaiLogic.GetTags();
        }

        private void ComboBox_tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hentaiLogic.GetSeries(comboBox_tags.SelectedItem.ToString());
        }
    }
}
