using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;
using HandyControl.Controls;

namespace Chart_Modeller
{
    public partial class ConnectDbPage : Page
    {
        public ConnectDbPage()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $@"Data Source={serverTxt.Text};Initial Catalog=master;Persist Security Info=True;User ID={loginTxt.Text};Password={passwordTxt.Text}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    CheckServer();
                }
                catch (SqlException)
                {
                    OpenWindow();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.PageName.Text = "Подключение к серверу";
        }

        private void AddServer()
        {
            MainWindow.Server = new Server()
            {
                ServerName = serverTxt.Text,
                Login = loginTxt.Text,
                Password = passwordTxt.Text
            };
            MainWindow.ServersList.Add(MainWindow.Server);
        }

        private void CheckServer()
        {
            foreach (var server in MainWindow.ServersList)
            {
                if (server.ServerName != serverTxt.Text || server.Login != loginTxt.Text || server.Password != passwordTxt.Text)
                {
                    AddServer();
                }
                MainWindow.MainFrameInstance.Navigate(new PanelsPage());
            }
        }

        private void OpenWindow()
        {
            bool isWindowOpen = false;

            foreach (System.Windows.Window w in Application.Current.Windows)
            {
                if (w is ErrorWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                ErrorWindow newwindow = new ErrorWindow("Ошибка при подключении к серверу");
                newwindow.Show();
            }
        }
    }
}
