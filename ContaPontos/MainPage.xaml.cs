using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ContaPontos.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO.IsolatedStorage;

namespace ContaPontos
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
        private string host;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (iso.Contains("host"))
                host = iso["host"].ToString().Trim();
            else
                //host = "10.2.25.7:5008";
                host = "192.168.0.102:11977";
            //onLoad(); 

        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string uri = "http://" + host + "/Pontos/getRetornarListaDesenvolvedores?sprint=104&semana=2";
            //string uri = "http://api.windows8central.in/api/books/";
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));
            client.DownloadStringCompleted += (s1, e1) =>
                {
                    var data = JsonConvert.DeserializeObject<Pontos[]>(e1.Result.ToString());
                    lstPontos.ItemsSource = data;
                };

            lblsprint.Text = "106";
            lblSemana.Text = "1";
        }
        
        private async void onLoad()
        {
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://10.2.25.7:5008/Pontos/getRetornarListaDesenvolvedores?sprint=106&semana=1");
                //HttpResponseMessage response = await client.GetAsync("http://192.168.0.103:11977/Pontos/RetornarListaSprints");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                this.lblsprint.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://192.168.0.103:11977/Pontos/RetornarListaSprints");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                this.lblsprint.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Config.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}