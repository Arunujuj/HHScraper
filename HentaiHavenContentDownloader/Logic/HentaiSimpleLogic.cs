using HentaiHavenContentDownloader.DataContexts;
using HHScraper;
using HHScraper.Interface;
using HHScraper.Models;
using HHScraper.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        public void GetEpisodes(Series series)
        {
            ViewModel.SERIES.Where(x => x == series).FirstOrDefault().EPISODES = scrapingContent.GetEpisodes(series);
        }

        public void LoadCache(string homePath)
        {
            ViewModel.TAGS = DeSerializeObject<ObservableCollection<string>>(homePath + @"\HHCD\tags.cache");
            ViewModel.SERIES = DeSerializeObject<ObservableCollection<Series>>(homePath + @"\HHCD\series.cache");
        }

        public void SaveCache(string homePath)
        {
            SerializeObject<ObservableCollection<string>>(ViewModel.TAGS, homePath + @"\HHCD\tags.cache");
            SerializeObject<ObservableCollection<Series>>(ViewModel.SERIES, homePath + @"\HHCD\series.cache");
        }

        public string GetVideoThumbnailURL(string directVideo)
        {
            return scrapingContent.GetVideoThumbnailURL(directVideo);
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

    }
}
