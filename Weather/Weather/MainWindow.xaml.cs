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
using System.Diagnostics;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String citySt;
        String temperature;
        String condition;
        String humidity;
        String windspeed;
        String town;
        String region;
        String country;
        String lastUpdate;
        string[] nextDay = new string[10];
        string[] nextCond = new string[10];
        string[] nextHigh = new string[10];
        string[] nextLow = new string[10];

        public MainWindow()
        {
            InitializeComponent();
            citySt = "Dayton, OH";
            updateUI();
        }
        // closes the app
        private void mnuCloseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool getWeather(String citySt)
        {
            //citySt = "dayton, oh";
            String st = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{0}\")", citySt);
            XmlDocument wData = new XmlDocument();
            try
            {
                wData.Load(st);
            }
            catch(Exception msg)
            {
                Debug.WriteLine("error getting weather");
                return false;
            }
            wData.Load(st);
            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", @"http://xml.weather.yahoo.com/ns/rss/1.0");
            XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
            temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
            condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
            humidity = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;
            windspeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;
            town = channel.SelectSingleNode("yweather:location", manager).Attributes["city"].Value;
            region = channel.SelectSingleNode("yweather:location", manager).Attributes["region"].Value;
            country = channel.SelectSingleNode("yweather:location", manager).Attributes["country"].Value;
            lastUpdate = channel.SelectSingleNode("item").SelectSingleNode("pubDate").InnerText;
            XmlNodeList forecast = channel.SelectSingleNode("item").SelectNodes("yweather:forecast", manager);

            for (int i=0; i<forecast.Count;i++)
            {
                nextDay[i] = forecast[i].Attributes["day"].Value;
                nextCond[i] = forecast[i].Attributes["text"].Value;
                nextHigh[i] = forecast[i].Attributes["high"].Value;
                nextLow[i] = forecast[i].Attributes["low"].Value;
            }

            return true;
        }

        private int FtoC(double F)
        {
            return (int)((F - 32) * 5.0 / 9.0);
        }

        private void btnGetWeather(object sender, RoutedEventArgs e)
        {
            citySt = tbCitySt.Text.ToString();
            updateUI();
        }

        private void updateUI()
        {
            if (!getWeather(citySt))
                return;
            labelTempF.Content = temperature + string.Format(" \u00B0") + "C";
            int tempC = FtoC(Double.Parse(temperature));

            labelTempC.Content = tempC.ToString() + string.Format(" \u00B0") + "F";
            labelCondition.Content = condition;
            labelHumidity.Content = humidity;
            labelWindSpeed.Content = windspeed;
            labelCountry.Content = country;
            labelCitySt.Content = town + ", " + region;
            labelNextDay.Content = nextDay[1];
            labelNextCondition.Content = nextCond[1];
            labelNextHigh.Content = nextHigh[1];
            labelNextLow.Content = nextLow[1];


            labelLastUpdated.Content = "Last Updated on : " + lastUpdate;
        }

        private void tbEnterKeyPressed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                citySt = tbCitySt.Text.ToString();
                updateUI();
            }
        }

        private void mnuAboutClick(object sender, RoutedEventArgs e)
        {

            WeatherAbout about = new WeatherAbout();
            about.Name = "Weather App";
            about.ShowDialog();
        }
    }
}
