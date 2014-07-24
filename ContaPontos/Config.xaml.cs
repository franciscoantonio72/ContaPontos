using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace ContaPontos
{
    public partial class Config : PhoneApplicationPage
    {
        private IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
        public Config()
        {
            InitializeComponent();

            if (iso.Contains("host"))
            {
                txtHost.Text = iso["host"].ToString();
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (iso.Contains("host"))
            {
                iso["host"] = txtHost.Text;
            }
            else
            {
                iso.Add("host", txtHost.Text);
            }
            iso.Save();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}