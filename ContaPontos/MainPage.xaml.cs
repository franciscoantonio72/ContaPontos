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
using ContaPontos.Model;
using System.Threading;

namespace ContaPontos
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
        private string host;
        private string Semana;
        private string Sprint;
        private string uri;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (iso.Contains("host"))
                host = iso["host"].ToString().Trim();
            else
                    host = "10.2.25.7:5008";
                //host = "10.2.25.202:11977";
                //host = "10.2.25.25:11977";
                //host = "169.254.80.80:11977";
            //host = "192.168.0.107:11977";

            uri = "http://" + host + "/Pontos/getRetornarSprintDaSemana";
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));
            client.DownloadStringCompleted += (s1, e1) =>
            {
                Sprint data = JsonConvert.DeserializeObject<Sprint>(e1.Result.ToString());
                Semana = data.NumeroSemana;
                Sprint = data.NumeroSprint;
                lblsprint.Text = Sprint;
                lblSemana.Text = Semana;
                RetornarListaDesenvolvedores();
            };
        }

        private void RetornarListaDesenvolvedores()
        {
            uri = "http://" + host + "/Pontos/getRetornarListaDesenvolvedores?sprint=" + Sprint + "&semana=" + Semana;
            WebClient client1 = new WebClient();
            client1.Headers["Accept"] = "application/json";
            client1.DownloadStringAsync(new Uri(uri));
            client1.DownloadStringCompleted += (s1, e1) =>
            {
                var data1 = JsonConvert.DeserializeObject<List<Pontos>>(e1.Result.ToString());
                lstPontos.ItemsSource = data1;
            };
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

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listaNome = lstPontos.SelectedItem as Pontos;
            string nomeDesenvolvedor = listaNome.Nome.ToString();
            string semanaDesenvolvedor = Semana;
            string sprintDesenvolvedor = Sprint;
            NavigationService.Navigate(new Uri( "/DescricaoOS.xaml?nome="+ nomeDesenvolvedor + "&sprint=" + sprintDesenvolvedor + "&semana=" + semanaDesenvolvedor + "&host=" + host, UriKind.Relative));
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
