using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using ContaPontos.Model;

namespace ContaPontos
{
    public partial class DescricaoOS : PhoneApplicationPage
    {
        public DescricaoOS()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string nomeDesenvolvedor = NavigationContext.QueryString["nome"];
            string sprintDesenvolvedor = NavigationContext.QueryString["sprint"];
            string semanaDesenvolvedor = NavigationContext.QueryString["semana"];
            string host = NavigationContext.QueryString["host"];

            var uri = "http://" + host + "/Pontos/getEnviarDados?nome=" + nomeDesenvolvedor + "&sprint=" + sprintDesenvolvedor + "&semana=" + semanaDesenvolvedor;
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));
            client.DownloadStringCompleted += (s1, e1) =>
            {
                var data = JsonConvert.DeserializeObject<List<Os>>(e1.Result.ToString());
                listaOS.ItemsSource = data;
            };
        }
    }
}